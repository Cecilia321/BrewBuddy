namespace BrewBuddy.Interface
{
    //Jeg laver her et repository interface med alle crud operationerne, som alle repositories kan arve fra. Dette er et generisk interface, det kan altså bruges af forskellige repositories 
    public interface IRepository<T> where T : class
    {
        //her er alle de metoder som alle repositories skal arve fra, jeg bruger entity ved add og update, da disse metoder kræver hele objektet. 
        void Add(T entity);
        void Update(T entity);
        void Delete(int Id);
        List<T> GetAll();
    }
}
