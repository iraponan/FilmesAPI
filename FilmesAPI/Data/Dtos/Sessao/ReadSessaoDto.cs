namespace FilmesAPI.Data.Dtos.Sessao {
    public class ReadSessaoDto {
        public int Id { get; set; }
        public DateTime DataEHoraDaSessao { get; set; }
        public int FilmeId { get; set; }
    }
}
