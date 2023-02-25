namespace TesteGPT.API.Models
{
    public class ChatGPTInputModel
    {
        public ChatGPTInputModel(string prompt)
        {
            //Aqui nesta implementação, estou dando um contexto para o chatGPT compreender melhor o que eu desejo, nesse caso, estou solicitando a quantidade de gols do jogador.
            //Poderia ser qualquer outra estatistica, como: Títulos, Lesões, etc.
            this.prompt = $"Tell me amount of goals of soccer player: {prompt} ";
            temperature = 0.2m;
            max_tokens = 100;
            model = "text-davinci-003";
        }

        public string model { get; set; }
        public string prompt { get; set; }
        public int max_tokens { get; set; }
        public decimal temperature { get; set; }
    }
}
