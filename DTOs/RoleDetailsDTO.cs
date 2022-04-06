namespace LibraryManagement.DTOs
{
   public class RoleDetailsDTO
    {
        public int roleId { get; set; }
        public int permissionId { get; set; }
        public bool isPermitted { get; set; }
    }
}
