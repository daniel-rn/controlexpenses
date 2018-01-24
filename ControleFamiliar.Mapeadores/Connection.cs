using System.Data.Common;
using FirebirdSql.Data.FirebirdClient;

namespace ControleFamiliar.Mapeadores
{
    public static class Connection
    {
        public static FbConnection FbCnn { get; private set; }

        public static FbCommandBuilder FbCmm { get; } = new FbCommandBuilder();

        public static bool Active(bool bActive)
        {
            if (bActive)
            {
                FbCnn = new FbConnection(ObtenhaConfiguracoesBanco());
                FbCnn.Open();
                return true;
            }
            FbCnn.Close();
            return false;
        }

        private static string ObtenhaConfiguracoesBanco()
        {
            var configuracoesBanco = "User=SYSDBA;Password=masterkey";
            configuracoesBanco += ";Database = C:\\WorkNascimento\\baseDeDados\\CONTROLEFAMILIAR.FDB";
            configuracoesBanco += ";Port=3050;Dialect=3;Charset=NONE;Role=;Connection lifetime=0;";
            configuracoesBanco += "Connection timeout=7;Pooling=True;Packet Size=8192;Server Type=0";
            return configuracoesBanco;
        }

        public static FbTransaction ObtenhaFbTransaction()
        {
            Active(true);
            return FbCnn.BeginTransaction();
        }

        public static DbCommand ObtehaComando(string sql) => new FbCommand(sql, FbCnn);
    }
}
