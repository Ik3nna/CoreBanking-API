using AutoMapper;
using CoreBanking.Core.Entities;

public class FullNameResolver : IValueResolver<Customer, object, string>

{

    public string Resolve(Customer source, object destination, string destMember, ResolutionContext context)

        => $"{source.FirstName} {source.LastName}";

}