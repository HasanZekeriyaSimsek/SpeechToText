using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Globalization;

namespace SpeechToText
{
    public partial class Form1 : Form
    {
        SpeechRecognitionEngine tanimaMotoru;
        public Form1()
        {
            InitializeComponent();
            SpeechMotoruKur();
        }

        private void SpeechMotoruKur() {

            try
            {
                // Türkçe (tr-TR) dil paketini kullanarak motoru başlat
                tanimaMotoru = new SpeechRecognitionEngine(new CultureInfo("en-US"));

                // Serbest dikte (Free Dictation) grameri yüklüyoruz. Bu, genel konuşmaları tanıması için gerekli.
                Grammar dictationGrammar = new DictationGrammar();
                tanimaMotoru.LoadGrammar(dictationGrammar);

                // Konuşma tanındığında çalışacak olayı (event) metoda bağlıyoruz
                tanimaMotoru.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(tanimaMotoru_SpeechRecognized);

                // Motorun varsayılan ses giriş cihazını kullanmasını ayarla (Mikrofonunuz)
                tanimaMotoru.SetInputToDefaultAudioDevice();

                // Form kapandığında motoru temizlemek için olayı bağla
                this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Konuşma motoru kurulamadı. Hata: " + ex.Message + "\n\nLütfen Türkçe dil paketinizin kurulu olduğundan emin olun.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Hata olursa butonu devre dışı bırak ki basılmasın
                if (btnDinle != null) btnDinle.Enabled = false;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (tanimaMotoru != null)
            {
                tanimaMotoru.RecognizeAsyncStop(); // Asenkron tanımayı durdur
                tanimaMotoru.Dispose();           // Kaynakları serbest bırak
            }
        }

        private void tanimaMotoru_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            // Tanıma güvenilirliği %85'in (0.85f) üzerindeyse metni ekle
            // Güvenilirlik düşükse saçma sapan kelimeleri eklemesini engelleriz
            if (e.Result.Confidence > 0.85f)
            {
                // Tanınan metni (e.Result.Text) txtSonuc TextBox'a ekle ve sonuna bir boşluk bırak
                txtSonuc.Text += e.Result.Text + " ";
            }
        }

        private void txtSonuc_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnDinle_Click(object sender, EventArgs e)
        {
            if (tanimaMotoru == null) return;

            try
            {
                // Dinlemeyi sürekli (Multiple) modda, asenkron olarak başlat
                tanimaMotoru.RecognizeAsync(RecognizeMode.Multiple);

                btnDinle.Text = "Dinleniyor...";
                btnDinle.Enabled = false; // Tekrar basılmasını engelle
                txtSonuc.Text = ""; // Yeni bir dinleme için metin kutusunu temizle
                MessageBox.Show("Dinleme başladı. Lütfen mikrofona konuşun.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dinleme başlatılamadı. Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnDinle.Enabled = true;
            }
        }
    }
}
