using BotAPI.Application.DTOs.FileReader.Responses;
using BotAPI.Application.Interfaces;
using BotAPI.Infrastructure.FileManager.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pinecone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotAPI.Infrastructure.FileManager.Services
{
    public class FileManagerService : IFileManagerService
    {
        private readonly string _pineconeApiKey;
        private readonly string _pineconeIndex;
        private readonly string _pineconeUrl;
        private readonly PineconeClient _pineconeClient;

        public FileManagerService(IConfiguration configuration)
        {
            _pineconeApiKey = configuration["Pinecone:ApiKey"];
            _pineconeIndex = configuration["Pinecone:Index"];
            _pineconeClient = new PineconeClient(_pineconeApiKey);
        }

        public async Task CreateVector(string ChannelId, FileOutputModel fileOutput)
        {
            var pinecone = _pineconeClient.Index(_pineconeIndex);

            var vectors = new List<Vector>();
            for (var i = 0; i < fileOutput.Contents.Count; i++)
            {
                var element = fileOutput.Contents.ElementAtOrDefault(i);

                vectors.Add(
                    new Vector
                    {
                        Id = Guid.NewGuid().ToString(),
                        Values = new(),
                        Metadata = new Metadata
                        {
                            ["PageNumber"] = element.PageNumber,
                            ["Content"] = element.Content
                        },
                    }
                );
            }


            await pinecone.UpsertAsync(new UpsertRequest { Vectors = vectors, Namespace = ChannelId});
        }
    }
}
