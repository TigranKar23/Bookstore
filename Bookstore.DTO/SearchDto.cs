using System;

namespace Bookstore.DTO

{
    public enum UserRole
    {
        User,
        Admin
    }
    public class SearchDto
    {
        public string? Search { get; set; } 
        public UserRole Role { get; set; }
    }
}
