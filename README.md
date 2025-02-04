# BİTİRME PROJESİ: DISCORD BOTU GELİŞTİRME RAPORU

## 1. Giriş

Bu bitirme projesinin amacı, bir Discord botu geliştirerek modern yazılım mimarisi ve teknolojilerini kullanarak pratik bir uygulama gerçekleştirmektir. Bot, kullanıcıların belirli metinler veya dosyalar üzerinden sorularını cevaplayabilen, makine öğrenmesi tabanlı bir sistem olacaktır. Proje iki dönem boyunca dört aşamada yürütülecek olup, her bir aşamada sistemin yeni özellikleri ve entegrasyonları üzerinde çalışılacaktır.

Projenin ilk aşamasında bot, sabit bir metinle eğitilecek, sonraki aşamalarda ise dosya okuma ve veri eğitimi eklenerek geliştirilmiş bir sistem ortaya çıkacaktır. İlerleyen aşamalarda ise mikroservis mimarisi, kimlik doğrulama, yük dengeleme ve ölçeklenebilirlik gibi konular üzerinde çalışılacaktır.

## 2. Proje Amaçları ve Kapsamı

Discord botunun temel amacı, kullanıcılardan gelen soruları belirli bir metin veya dosya üzerinden yanıtlamak ve bu süreçte modern yazılım araçlarını kullanmaktır. Proje dört ana başlığa ayrılarak yürütülecektir:

- **Güz Dönemi Vize**: Botun temel işlevlerinin geliştirilmesi.
- **Güz Dönemi Final**: Pinecone entegrasyonu ve sabit metin ile botun eğitilmesi.
- **Bahar Dönemi**: Çeşitli teknolojilerle entegrasyon.

## 3. Güz Dönemi Vize

### 3.1 Aşamanın Hedefi

Bu aşamada, Discord botunun temel yapısı oluşturulacak ve bot, belirli bir sabit metin üzerinden kullanıcılardan gelen soruları yanıtlayacaktır. Amaç, temel fonksiyonların çalışır hale getirilmesi ve botun, ilk aşamada metne dayalı bir öğrenme süreci gerçekleştirmesidir.

### 3.2 Kullanılan Teknolojiler
- **.NET Core**: Botun arka plan işleyişini ve temel fonksiyonlarını geliştirmek için kullanılacak.
- **Gemini**: Kullanıcıların sorularına sabit bir metin üzerinden cevap verebilmek için botu eğitecek sistem.
- **Discord API**: Botun Discord sunucularında çalışmasını ve kullanıcılarla etkileşim kurmasını sağlayacak araç.

### 3.3 Gerçekleştirilen Çalışmalar
- **Botun temel fonksiyonları geliştirildi:** Discord API entegrasyonu yapılarak bot, sunuculara eklenebilir hale getirildi.
- **Sabit metin ile eğitim sağlandı:** İlk aşamada, botun belirli bir metin üzerinden sorulara cevap verebilmesi için sabit bir içerikle eğitildi.
- **Kullanıcıdan gelen sorulara cevap verme mekanizması:** Kullanıcılar bot ile etkileşime geçerek, sabit metin üzerinden yanıtlar alabildiler.

---

## 4. Güz Dönemi Final

### 4.1 Aşamanın Hedefi
Bu aşamada, botun sadece sabit bir metin yerine, bir dosya üzerinden öğrenme yeteneği kazandırılması hedeflenmiştir. Dosya okuma fonksiyonu eklenerek bot, bu dosyanın içeriğini okuyacak ve bu içerik üzerinden sorulara yanıt verebilecektir. Aynı zamanda, Pinecone entegrasyonu ile verilerin vektörel bir veritabanında saklanıp kullanılmasına yönelik çalışmalar başlatılacaktır.

### 4.2 Kullanılan Teknolojiler
- **.NET Core**: Botun gelişmiş dosya okuma fonksiyonlarını ve işlevlerini geliştirmek için kullanılacak.
- **Gemini**: Kullanıcıların sorularına dosya üzerinden cevap verebilmesi için botu eğitecek.
- **Discord API**: Botun kullanıcılarla olan etkileşimlerini yönetmeye devam edecek.
- **Pinecone**: Botun dosya içeriklerini vektörel bir veritabanında saklayıp öğrenme sürecine katkı sağlamak için kullanılacak.

### 4.3 Gerçekleştirilen Çalışmalar
- **Dosya okuma fonksiyonu geliştirildi:** Kullanıcıların bot ile dosya yükleyerek botu eğitmesi sağlandı.
- **Pinecone entegrasyonu başlatıldı:** Dosya içeriklerinin vektörel verilere çevrilip Pinecone ile saklanması ve eğitilmesi üzerine çalışmalar yapıldı.
---

## 5. Bahar Dönemi

### 5.1 Aşamanın Hedefi
Bahar döneminde botun mimarisi **Clean Architecture** altyapısına geçirilecek ve farklı teknolojiler ile entegre hale getirilecektir. Bu sayede botun modülerliği artırılacak, her bir fonksiyon bağımsız katmanlar olarak çalıştırılabilecektir. Ayrıca mesajlaşma altyapısı, kimlik doğrulama, veri yönetimi ve hata yönetimi gibi kritik konular bu aşamada ele alınacaktır.

### 5.2 Teknolojiler ve Araçlar
- **Redis**: Hızlı veri paylaşımı ve önbellekleme çözümü olarak kullanılacak.
- **Microsoft Identity**: Merkezi kimlik doğrulama ve yetkilendirme sağlanarak, her bir katmanın güvenliği ve kullanıcı doğrulaması yapılacak.
- **Docker**: Katmanların containerize edilerek bağımsız ve izole bir şekilde çalıştırılması sağlanacak.
- **Loglama**: Sistemdeki önemli olayların kaydedilmesi ve izlenmesi için kullanılacak.
- **Exception Middleware**: Hataların merkezi bir şekilde yönetilmesi ve loglanması sağlanacak.

### 5.3 Gerçekleştirilen Çalışmalar
1. **Clean Architecture geçişi**: Proje, farklı fonksiyonlar için Clean Architecture prensiplerine uygun olarak yeniden tasarlandı.
2. **Redis entegrasyonu**: Veri paylaşımı ve önbellekleme için Redis kullanıldı, performans artırıldı.
3. **Microsoft Identity entegrasyonu**: Kimlik doğrulama ve yetkilendirme süreçleri güvenli bir şekilde yönetildi.
4. **Docker ile containerize yapı**: Her bir katman, Docker container'ları içinde bağımsız çalışacak şekilde yapılandırıldı.
5. **Loglama altyapısı**: Uygulama olaylarının izlenebilirliğini artırmak için loglama entegrasyonu sağlandı.
6. **Exception Middleware entegrasyonu**: Hataların merkezi yönetimi ve loglanması sağlanarak hata takibi kolaylaştırıldı.

---

## 6. Sonuç

Bu proje, Discord botunun temel işlevlerinden başlayarak, ileri seviye **Clean Architecture** ve çeşitli teknolojilerle entegre bir yapıya dönüştürülmesini kapsamaktadır. Geliştirilen bot, modüler ve ölçeklenebilir bir yapıya kavuşarak, hem sabit metin hem de dosya içeriklerine dayalı olarak kullanıcı sorularına yanıt verebilmektedir. Gelecek çalışmalarda, botun kapsamı genişletilebilir ve ek özellikler entegre edilerek daha gelişmiş bir sistem haline getirilebilir.

