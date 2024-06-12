namespace HashTable.Classes;

public enum Year
{
    Junior,
    Fourth,
    Fifth,
    Sixth,
    LowerEighth,
    UpperEighth
};

public class Student : IEquatable<Student>
{

    public string FirstName { get; }
    public string LastName { get; }
    public DateOnly DateOfBirth { get; }
    public Year YearGroup { get; set; }

    public Student(string firstName, string lastName, DateOnly dateOfBirth, Year yearGroup)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new ArgumentException("The first name cannot be null or white space.", nameof(firstName));
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new ArgumentException("The last name cannot be null or white space.", nameof(lastName));
        }

        if (!firstName.All(char.IsLetter))
        {
            throw new ArgumentException("The first name must be letters.", nameof(firstName));
        }

        if (!lastName.All(char.IsLetter))
        {
            throw new ArgumentException("The last name must be letters.", nameof(lastName));
        }

        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        YearGroup = yearGroup;
    }

    public override string ToString()
    {
        return $"First Name {FirstName}; Last Name {LastName}; Date of Birth {DateOfBirth}; Year Group {YearGroup}";
    }

    public override int GetHashCode()
    {
        HashCode hash = new();
        hash.Add(FirstName);
        hash.Add(LastName);
        hash.Add(DateOfBirth);
        return hash.ToHashCode();
    }

    // I referred to the Microsoft Document to implement IEquatable and Equals method.

    public override bool Equals(object? obj)
    {
        return obj is Student s && Equals(s);
    }

    public bool Equals(Student? other)
    {
        if (other is null)
        {
            return false;
        }
        return other.FirstName == FirstName && other.LastName == LastName && other.DateOfBirth == DateOfBirth;
    }

    public static bool operator ==(Student student1, Student student2)
    {
        if (((object)student1) == null || ((object)student2) == null)
        {
            return Equals(student1, student2);
        }

        return student1.Equals(student2);
    }

    public static bool operator !=(Student student1, Student student2)
    {
        if (((object)student1) == null || ((object)student2) == null)
        {
            return !Equals(student1, student2);
        }

        return !student1.Equals(student2);
    }
}
