namespace CrimeAlert.DataContracts
{
    public interface IUnitOfWorkRepository
    {
        void Use(IUnitOfWork unitOfWork);
    }
}
