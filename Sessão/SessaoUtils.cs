using Microsoft.AspNetCore.Mvc;

namespace RoofStockBackend.Sessão
{
    public class SessaoUtils
    {
        #region Contexto de Sessão - Usuário
        public static string GetUserId(HttpContext httpContext)
        {
            return httpContext.Session.GetString("UserId") ?? "";
        }
        public static void SetUserId(string userId, HttpContext httpContext)
        {
            httpContext.Session.SetString("UserId", userId);
        }
        #endregion

        #region Contexto de Sessão - Estoque
        public static string GetStockId(HttpContext httpContext)
        {
            return httpContext.Session.GetString("StockId") ?? "";
        }
        public static void SetStockId(string stockId, HttpContext httpContext)
        {
            httpContext.Session.SetString("StockId", stockId);
        }
        #endregion  
    }
}
