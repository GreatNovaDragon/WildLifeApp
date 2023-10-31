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


        public Task<Pokemon> GetPokemon(string id)
        {
            return context.Pokedex
            .Include(p => p.Abilities).Include(p => p.Type1).Include(p => p.Type2).FirstOrDefaultAsync(m => m.ID == id);
        }

        public Task<List<Learnset>> GetLearnset(Pokemon pkmn)
        {
            return Task.FromResult(context.Learnsets.Include(p => p.mon).Include(m => m.move).ThenInclude(mv => mv.type)
            .Include(m => m.move).ThenInclude(mv => mv.MoveClass).Where(m => m.mon == pkmn).ToList());

        }



    }
}
