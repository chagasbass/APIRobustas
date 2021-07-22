using ApiRobustas.Compartilhados.Configuracoes;
using Microsoft.Extensions.Options;
using Microsoft.Win32.SafeHandles;
using System;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace ApiRobustas.Infraestrutura.Data.ContextosDeDados
{
    public class ContextoDeDadosDapper : IDisposable
    {
        private readonly ConfiguracoesBaseOptions _configuracoesBaseOptions;
        public SqlConnection Conexao { get; private set; }

        private readonly SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        public ContextoDeDadosDapper(IOptionsMonitor<ConfiguracoesBaseOptions> options)
        {
            _configuracoesBaseOptions = options.CurrentValue;
            Conexao = new SqlConnection(_configuracoesBaseOptions.BaseDeDados);
        }

        public bool AbrirConexao()
        {
            try
            {
                if (Conexao.State != System.Data.ConnectionState.Open)
                    Conexao.OpenAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        #region DisposePattern

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                _safeHandle?.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
