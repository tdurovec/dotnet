namespace MojaKniznica;
public record Teacher(
    string FirstName, 
    string LastName, 
    DateTime? Birthdate,
    Gender Gender
) : Person(FirstName, LastName, Birthdate, Gender);