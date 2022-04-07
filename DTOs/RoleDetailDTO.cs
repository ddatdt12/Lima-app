namespace LibraryManagement.DTOs
{
   public class RoleDetailDTO
    {
        public int roleId { get; set; }
        public int permissionId { get; set; }
        public bool isPermitted { get; set; }
    }
}
