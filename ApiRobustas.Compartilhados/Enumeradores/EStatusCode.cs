namespace ApiRobustas.Compartilhados.Enumeradores
{
    public class EStatusCode : Enumeration
    {
        public static EStatusCode BadRequest { get; } = new EStatusCode(400, nameof(BadRequest));
        public static EStatusCode NotFound { get; } = new EStatusCode(404, nameof(NotFound));
        public static EStatusCode Post { get; } = new EStatusCode(201, nameof(Post));
        public static EStatusCode Get { get; } = new EStatusCode(200, nameof(Get));
        public static EStatusCode Put { get; } = new EStatusCode(204, nameof(Put));
        public static EStatusCode Patch { get; } = new EStatusCode(200, nameof(Patch));
        public static EStatusCode Delete { get; } = new EStatusCode(200, nameof(Delete));

        public EStatusCode(int id, string name) : base(id, name) { }
    }
}
