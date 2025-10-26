# 🔊 SpeechToText: Konuşmadan Metne Çeviri Uygulaması (C# Windows Forms)

Bu proje, C# ve Windows Forms kullanılarak geliştirilmiş basit bir konuşmadan metne çeviri (Speech-to-Text) uygulamasıdır. Kullanıcının mikrofona söylediği kelimeleri dinler ve gerçek zamanlı olarak bir metin kutusuna yazar.

## 🌟 Özellikler

* **Gerçek Zamanlı Tanıma:** `System.Speech.Recognition` kütüphanesi aracılığıyla sesi anlık olarak metne çevirir.
* **Serbest Dikte Grameri:** Geniş bir kelime dağarcığı ve serbest konuşma tanıma desteği.
* **Türkçe/İngilizce Desteği:** `tr-TR` ve `en-US` kültürlerini kolayca değiştirebilme imkanı.

## 🛠️ Teknolojiler

* **Geliştirme Ortamı:** Visual Studio 2022
* **Programlama Dili:** C#
* **Arayüz:** Windows Forms App (.NET Framework 4.8)
* **Temel Kütüphane:** `System.Speech`

## 🚀 Kurulum ve Çalıştırma

Projenin başarıyla çalışması için hem kod kurulumu hem de Windows sistem ayarlarının yapılması gerekmektedir.

### 1. Sistem Gereksinimleri (Kritik!)

Bu uygulama Windows'un yerleşik konuşma tanıma özelliğini kullandığı için, kullanmak istediğiniz dilin paketi kurulu olmalıdır.

* **Mikrofon:** Çalışır durumda ve ses seviyesi ayarlanmış bir mikrofon (Windows ses ayarlarında %20'nin üzerinde ses seviyesi önerilir).
* **Dil Paketi:** Windows **Ayarlar → Zaman ve Dil → Dil ve Bölge** kısmında, kullanmak istediğiniz dilin (Türkçe veya İngilizce) **Konuşma (Speech)** özelliğinin kurulu olduğundan emin olun.

### 2. Proje Kurulumu

1. Projeyi Visual Studio 2022'de açın.
2. **Referans Kontrolü:** Solution Explorer'da References (Referanslar) altında `System.Speech` kütüphanesinin ekli olduğundan emin olun. (Eksikse, References'a sağ tıklayıp Add Reference ile ekleyin).

### 3. Kullanım

1. Visual Studio'da **F5** tuşuna basarak uygulamayı başlatın.
2. Açılan formda **"Dinlemeyi Başlat"** butonuna tıklayın.
3. Uygulama artık dinlemededir. Mikrofona konuştuğunuz kelimeler (koddaki `CultureInfo` ayarınıza göre İngilizce veya Türkçe olarak) alttaki metin kutusuna yazılacaktır.

## ⚙️ Kod Yapısı

| Dosya / Bileşen | Açıklama |
| :--- | :--- |
| `Form1.cs` | Uygulamanın tüm mantığını barındıran ana dosya. |
| `tanimaMotoru` | `SpeechRecognitionEngine` sınıfından oluşturulmuş, konuşma tanımayı yöneten nesne. |
| `txtSonuc` | Konuşulan metnin yazıldığı TextBox bileşeni. |
| `btnDinle` | Dinleme işlemini başlatan Button bileşeni. |
| `SpeechMotoruKur()` | Motoru başlatan, `CultureInfo`'yu (`tr-TR` / `en-US`) ayarlayan ve `DictationGrammar`'ı yükleyen metot. |
| `tanimaMotoru_SpeechRecognized` | Konuşma tanındığında tetiklenen olay (Event). Tanınan metni güvenilirlik kontrolünden geçirerek (`confidence > 0.50f`) `txtSonuc`'a yazar. |