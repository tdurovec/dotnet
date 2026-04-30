
namespace MojaKniznica;

public record Student(
    string FirstName, 
    string LastName, 
    DateTime? Birthdate, 
    Gender Gender, 
    List<Subject> EnrolledSubjects
) : Person(FirstName, LastName, Birthdate, Gender );