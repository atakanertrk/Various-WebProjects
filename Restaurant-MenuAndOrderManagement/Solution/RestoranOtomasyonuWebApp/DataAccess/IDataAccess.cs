using RestoranOtomasyonuWebApp.Models;
using System;
using System.Collections.Generic;

namespace RestoranOtomasyonuWebApp.DataAccess
{
    public interface IDataAccess
    {
        void ChangeGurupNameById(int gurupId, string yeniGurupAdi);
        string GetGurupNameByGurupId(int id);
        List<UrunModel> GetUrunByGurupId(int gurupid);
        UrunModel UrunBilgileriByUrunId(int urunId);
        string GetYoneticiSifresi();
        public SiteEkstraModel GetSiteEkstra();
        public void SetSiteEkstra(SiteEkstraModel siteEkstra);
        string GetSiparisIslemleriGirisSifresi();
        List<UrunModel> GurupIcindekiUrunlerinIdListesi(int gurupId);
        public void MasaEkle(int masaNo);
        public void MasaSil(int masaNo);
        public void MasaNotuSil(int masaNo);
        public void MasaNotuEkle(int masaNo, string masaNotu);
        public decimal MasaToplamUcreti(int masaNo);
        public string GetMasaNotu(int masaNo);
        public List<int> GetMasaNumaralariList();
        List<SiparisModel> GetMasaSiparisleri(int masaNo);
        public void SiparisEkle(int masaNo, int urunId, decimal adet, string siparisNotu);
        public List<string> TamamlanmisUrunAdlariByDate(DateTime date);
        public decimal TamamlanmisUrununMiktariByDate(DateTime date, string urunadi);
        public string GetSiparisNotu(int siparisId);
        public void SiparisSil(int siparisId);
        public void MasaSiparisleriniSil(int masaNo);
        public void TamamlanmislaraEkle(string urunAdi, decimal urunFiyati, decimal adet,int masaNo);
        public List<TamamlanmisSiparisModel> GetTamamlanmisSiparislerByDate(System.DateTime date);
        public void StokDurumuDegistir(int urunId, string yeniStokDurumu);
        void UrunBilgileriGuncelle(UrunModel urunModel);
        int UrunEkle(UrunModel urunModel);
        void UrunGurubunuSil(int id);
        void SiparisTeslimEdildi(int siparisId);
        List<UrunGurubuModel> UrunGuruplari();
        void UrunSilById(int id);
        int YeniGurupEkle(string yeniGurupAdi);
    }
}