using System.Collections.Generic;
using ZMO_Eskisehir_WebUI.Models;

namespace ZMO_Eskisehir_WebUI.DataAccess
{
    public interface IDataAccess
    {
        List<BasinBultenlerimizModel> ByIdGetBasinBulteni(int id);
        List<BasindaBizModel> ByIdGetBasindaBiz(int id);
        List<DuyuruModel> ByIdGetDuyuru(int id);
        List<EgitimlerModel> ByIdGetEgitim(int id);
        List<EtkinlikModel> ByIdGetEtkinlik(int id);
        List<HaberModel> ByIdGetHaber(int id);
        void DeleteBasinBulteni(int id);
        void DeleteBasindaBiz(int id);
        void DeleteDuyuru(int id);
        void DeleteEgitim(int id);
        void DeleteEtkinlik(int id);
        void DeleteHaber(int id);
        List<BasinBultenlerimizModel> GetBasinBultenlerimiz();
        List<BasindaBizModel> GetBasindaBizKayitlar();
        List<DuyuruModel> GetDuyurular();
        List<EgitimlerModel> GetEgitimler();
        List<EtkinlikModel> GetEtkinlikler();
        List<HaberModel> GetHaberler();
        List<LoginModel> GetSifre();
        int InsertBasinBulteni(BasinBultenlerimizModel basinBulteni);
        int InsertBasindaBiz(BasindaBizModel basindaBiz);
        int InsertDuyuru(DuyuruModel duyuru);
        int InsertEgitim(EgitimlerModel egitim);
        int InsertEtkinlik(EtkinlikModel etkinlikModel);
        int InsertHaber(HaberModel haberModel);
        List<BasinBultenlerimizModel> SearchBasinBulteni(string searchValue);
        List<BasindaBizModel> SearchBasindaBiz(string searchValue);
        List<DuyuruModel> SearchDuyuru(string searchValue);
        List<EgitimlerModel> SearchEgitim(string searchValue);
        List<EtkinlikModel> SearchEtkinlik(string searchValue);
        List<HaberModel> SearchHaber(string searchValue);
        void UpdateBasinBulteni(BasinBultenlerimizModel basinBulteni);
        void UpdateBasindaBiz(BasindaBizModel basindaBiz);
        void UpdateDuyuru(DuyuruModel duyuru);
        void UpdateEgitim(EgitimlerModel egitim);
        void UpdateEtkinlik(EtkinlikModel etkinlik);
        void UpdateHaber(HaberModel haber);
    }
}