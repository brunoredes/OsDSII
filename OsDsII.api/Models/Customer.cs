using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OsDsII.api.DTO.Builder;
using OsDsII.api.DTO;

namespace OsDsII.api.Models
{
    [Index(nameof(Email), IsUnique = true)]
    [PrimaryKey(nameof(Id))]
    [Table("customer")]
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        [Comment("Customer ID as a primary key")]
        public int Id;

        [Required]
        [StringLength(60)]
        [Column("name")]
        [NotNull]
        [Comment("Customer name")]
        public string Name { get; set; } = null!;

        [Required]
        [Column("email")]
        [StringLength(100)]
        [BindRequired]
        [EmailAddress]
        [NotNull]
        [Comment("Customer email")]
        public string Email { get; set; } = null!;

        [Required]
        [Column("phone")]
        [Comment("Customer phone number")]
        [StringLength(20)]
        [AllowNull]
        public string Phone { get; set; }

        public List<ServiceOrder> ServiceOrders { get; set; }

        public Customer()
        { }

        public Customer(string name)
        {
            Name = name;
        }

        public Customer(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public Customer(string name, string email, string phone)
        {
            Name = name;
            Email = email;
            Phone = phone;
        }

        public Customer(int id, string name, string email, string phone)
        {
            Id = id;
            Name = name;
            Email = email;
            Phone = phone;
        }

        public CustomerDTO ToCustomer()
        {
            CustomerDTO customerDto = new CustomerDtoBuilder()
                    .WithId(Id)
                    .WithName(Name)
                    .WithEmail(Email)
                    .WithPhone(Phone)
                    .Build();
            return customerDto;
        }

        public override bool Equals(object? obj)
        {
            return obj is Customer customer &&
                   Id == customer.Id &&
                   Name == customer.Name &&
                   Email == customer.Email &&
                   Phone == customer.Phone &&
                   EqualityComparer<List<ServiceOrder>>.Default.Equals(ServiceOrders, customer.ServiceOrders);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Email, Phone, ServiceOrders);
        }
    }
}