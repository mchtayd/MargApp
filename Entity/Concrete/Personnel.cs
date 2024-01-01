namespace Entity.Concrete;

public class Personnel : IEntity
{
    public string SicilNo { get; set; }
    public string Name { get; set; }
    public int RoleId { get; set; }
    public string RoleName { get; set; }
    public DateTime JobStartDate { get; set; }
    public string Location { get; set; }
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; }
    public int UnitId { get; set; }
    public string UnitName { get; set; }
    /// <summary>
    /// Permission Values. Example Value => "1;3;24"
    /// </summary>
    public string Permissions { get; set; }
    public bool NeedToChangePassword { get; set; }

    public override string ToString()
    {
        return $"Sicil No: {SicilNo}, Name: {Name}, Role Id: {RoleId}, Role Name: {RoleName}, Department: {DepartmentName}, Unit: {UnitName}, " +
            $"Permissions: {Permissions}, NeedToChangePassword: {NeedToChangePassword}";
    }
}

