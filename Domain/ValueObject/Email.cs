namespace SeuPet.Domain.ValueObject;

public class Email
{
    public string Value { get; init; } 
    private Email() {}
    
    public static implicit operator string(Email value)
        => value.Value;
    
    public static explicit operator Email(string value)
    {
        if(string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("O email deve ser preenchido");
        
        if(!value.Contains("@"))
            throw new ArgumentException("O valor deve ser um email.");
        
        return new Email() { Value =  value.ToLower() };
    }
    
    public static bool operator ==(Email e1, Email e2)
    {
        if (ReferenceEquals(e1, e2)) return true;
        if (e1 is null || e2 is null) return false;
        return false;
    }
    
    public static bool operator !=(Email e1, Email e2)
     => !(e1 == e2);
    
    public override bool Equals(object obj)
    {
        if (obj is Email outro)
            return this == outro;
        
        return false;
    } 
}