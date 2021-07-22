namespace ApiRobustas.Testes.Bases
{
    public interface IFake<T>
    {
        T CriarEntidadeValida();
        T CriarEntidadeInvalida();
    }
}