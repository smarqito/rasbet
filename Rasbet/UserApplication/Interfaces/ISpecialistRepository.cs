namespace UserApplication.Interfaces;

public interface ISpecialistRepository
{

	Task<Specialist> RegisterSpecialist(string name, string email, string password, string language);

}