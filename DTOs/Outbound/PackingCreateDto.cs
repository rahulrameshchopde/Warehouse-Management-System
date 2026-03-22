public class PackingCreateDto
{
    public int OrderID { get; set; }
    public string PackageType { get; set; } = string.Empty;
    public double Weight { get; set; }
}