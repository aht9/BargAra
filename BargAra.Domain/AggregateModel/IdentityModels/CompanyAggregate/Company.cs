using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using BargAra.Domain.AggregateModel.RoleAggregate;
using BargAra.Domain.SeedWork;

namespace BargAra.Domain.AggregateModel.IdentityModels.CompanyAggregate;

public class Company : Entity, IAggregateRoot
{
    [MaxLength(100)] [Required] public string Name { get; private set; }

    [MaxLength(100)] public string? Family { get; private set; }

    [MaxLength(50)] public string? NickName { get; private set; }

    public bool? HasPublishPic { get; private set; }

    [MaxLength(300)] [Required] public string Address { get; private set; }

    [MaxLength(25)] public string? PhoneNumber { get; private set; }

    [MaxLength(25)] public string? Telephone { get; private set; }

    [StringLength(11, MinimumLength = 10)]
    [Required]
    public string? NationalId { get; private set; }

    [MaxLength(10)] public string? PostalCode { get; private set; }

    [MaxLength(200)] public string? NameEn { get; private set; }

    [MaxLength(100)] public string? Slogan { get; private set; }

    [MaxLength(200)] public string? SloganEn { get; private set; }

    [MaxLength(300)] public string? AddressEn { get; private set; }

    [MaxLength(100)] public string? NickNameEn { get; private set; }

    [Column(TypeName = "decimal(10,8)")] public decimal? GoogleMapLat { get; private set; }

    [Column(TypeName = "decimal(10,8)")] public decimal? GoogleMapLng { get; private set; }

    [Required] public bool IsReal { get; private set; }

    public long? ParentId { get; private set; }
    public Company Parent { get; private set; }
    public virtual ICollection<Company> Children { get; private set; } = new List<Company>();
    
    private readonly List<Role> _roles = new List<Role>();
    public virtual IReadOnlyCollection<Role> Roles => _roles.AsReadOnly();

    // Constructor for creating a new Company
    public Company(string name, string address, string nationalId, bool isReal)
    {
        SetName(name);
        SetAddress(address);
        SetNationalId(nationalId);
        IsReal = isReal;
    }

    public Company(string name, string address, string nationalId, bool isReal, string? family = null,
        string? phoneNumber = null, string? telephone = null, string? postalCode = null, string? nameEn = null,
        string? slogan = null, string? sloganEn = null, string? addressEn = null, string? nickNameEn = null,
        decimal? googleMapLat = null, decimal? googleMapLng = null)
    {
        SetName(name);
        SetAddress(address);
        SetNationalId(nationalId);
        if (family != null) SetFamily(family);
        if (phoneNumber != null) SetPhoneNumber(phoneNumber);
        if (telephone != null) SetTelephone(telephone);
        if (postalCode != null) SetPostalCode(postalCode);
        if (nameEn != null) SetNameEn(nameEn);
        if (slogan != null) SetSlogan(slogan);
        if (sloganEn != null) SetSloganEn(sloganEn);
        if (addressEn != null) SetAddressEn(addressEn);
        if (nickNameEn != null) SetNickNameEn(nickNameEn);
        if (googleMapLat != null) SetGoogleMapLat(googleMapLat);
        if (googleMapLng != null) SetGoogleMapLng(googleMapLng);
        IsReal = isReal;
    }

    // Business logic methods

    public void AddRole(string roleName, string DisplayName)
    {
        var role = new Role(roleName, DisplayName);
        typeof(Role)
            .GetProperty("CompanyId", BindingFlags.Instance | BindingFlags.NonPublic)
            ?.SetValue(role, this.Id);
        
        _roles.Add(role);
    }

    public void RemoveRole(string roleName)
    {
        var role = _roles.FirstOrDefault(r => r.Name == roleName);
        if (role == null)
            throw new InvalidOperationException($"Role {roleName} does not exist.");
        _roles.Remove(role);
    }
    
    public void SetFamily(string? family)
    {
        if (family != null && family.Length > 100)
            throw new ArgumentException("Family cannot exceed 100 characters.");
        Family = family;
    }

    public void SetPhoneNumber(string? phoneNumber)
    {
        if (phoneNumber != null && phoneNumber.Length > 25)
            throw new ArgumentException("Phone number cannot exceed 25 characters.");
        PhoneNumber = phoneNumber;
    }

    public void SetTelephone(string? telephone)
    {
        if (telephone != null && telephone.Length > 25)
            throw new ArgumentException("Telephone cannot exceed 25 characters.");
        Telephone = telephone;
    }

    public void SetPostalCode(string? postalCode)
    {
        if (postalCode != null && postalCode.Length > 10)
            throw new ArgumentException("Postal code cannot exceed 10 characters.");
        PostalCode = postalCode;
    }

    public void SetNameEn(string? nameEn)
    {
        if (nameEn != null && nameEn.Length > 200)
            throw new ArgumentException("NameEn cannot exceed 200 characters.");
        NameEn = nameEn;
    }

    public void SetSlogan(string? slogan)
    {
        if (slogan != null && slogan.Length > 100)
            throw new ArgumentException("Slogan cannot exceed 100 characters.");
        Slogan = slogan;
    }

    public void SetSloganEn(string? sloganEn)
    {
        if (sloganEn != null && sloganEn.Length > 200)
            throw new ArgumentException("SloganEn cannot exceed 200 characters.");
        SloganEn = sloganEn;
    }

    public void SetAddressEn(string? addressEn)
    {
        if (addressEn != null && addressEn.Length > 300)
            throw new ArgumentException("AddressEn cannot exceed 300 characters.");
        AddressEn = addressEn;
    }

    public void SetNickNameEn(string? nickNameEn)
    {
        if (nickNameEn != null && nickNameEn.Length > 100)
            throw new ArgumentException("NickNameEn cannot exceed 100 characters.");
        NickNameEn = nickNameEn;
    }

    public void SetGoogleMapLat(decimal? googleMapLat)
    {
        if (googleMapLat < -90 || googleMapLat > 90)
            throw new ArgumentException("GoogleMapLat must be between -90 and 90.");
        GoogleMapLat = googleMapLat;
    }

    public void SetGoogleMapLng(decimal? googleMapLng)
    {
        if (googleMapLng < -180 || googleMapLng > 180)
            throw new ArgumentException("GoogleMapLng must be between -180 and 180.");
        GoogleMapLng = googleMapLng;
    }

    public void SetHasPublishPic(bool? hasPublishPic)
    {
        HasPublishPic = hasPublishPic;
    }


    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.");
        if (name.Length > 100)
            throw new ArgumentException("Name cannot exceed 100 characters.");
        Name = name;
    }

    public void SetAddress(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Address cannot be empty.");
        if (address.Length > 300)
            throw new ArgumentException("Address cannot exceed 300 characters.");
        Address = address;
    }

    public void SetNationalId(string nationalId)
    {
        if (string.IsNullOrWhiteSpace(nationalId) || nationalId.Length < 10 || nationalId.Length > 11)
            throw new ArgumentException("National ID must be between 10 and 11 characters.");
        NationalId = nationalId;
    }

    public void SetNickName(string? nickName)
    {
        if (nickName != null && nickName.Length > 50)
            throw new ArgumentException("NickName cannot exceed 50 characters.");
        NickName = nickName;
    }

    public void AddChild(Company child)
    {
        if (child == null)
            throw new ArgumentNullException(nameof(child));
        if (Children.Any(c => c.Id == child.Id))
            throw new InvalidOperationException("Child already exists.");
        child.ParentId = this.Id;
        child.Parent = this;
        Children.Add(child);
    }

    public void RemoveChild(Company child)
    {
        if (child == null)
            throw new ArgumentNullException(nameof(child));
        if (!Children.Remove(child))
            throw new InvalidOperationException("Child does not exist.");
        child.ParentId = null;
        child.Parent = null;
    }
}