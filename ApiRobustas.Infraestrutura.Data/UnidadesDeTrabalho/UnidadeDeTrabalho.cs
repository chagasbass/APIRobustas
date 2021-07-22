using ApiRobustas.Dominio.UnidadeDeTrabalho;
using ApiRobustas.Infraestrutura.Data.ContextosDeDados;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ApiRobustas.Infraestrutura.Data.UnidadesDeTrabalho
{
    public class UnidadeDeTrabalho : IUnidadeDeTrabalho
    {
        private readonly ContextoDeDadosEfCore _Contexto;
        private IDbContextTransaction _TransacaoContexto { get; set; }

        private readonly SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        public UnidadeDeTrabalho(ContextoDeDadosEfCore contexto)
        {
            _Contexto = contexto;
        }

        public async Task CriarTransacaoAsync() => _TransacaoContexto = await _Contexto.Database.BeginTransactionAsync();

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

        public async Task CommitWithTransactionAsync()
        {
            await _TransacaoContexto.CommitAsync();
            await _TransacaoContexto.DisposeAsync();
        }

        public async Task RollbackWithTransactionAsync()
        {
            await _TransacaoContexto.RollbackAsync();
            await _TransacaoContexto.DisposeAsync();
        }

        public async Task CommitAsync() => await _Contexto.SaveChangesAsync();
    }
}
