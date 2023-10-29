using Microsoft.EntityFrameworkCore;
namespace WildLifeApp.Data
{
    public class PokemonService
    {

        ApplicationDbContext context = new ApplicationDbContext();



        public Task<Pokemon[]> GetPokemonList(int how_many = int.MaxValue)
        {

            return Task.FromResult(context.Pokedex.Take(how_many).OrderBy(index => index.Order).ToArray());
        }



    }
}
