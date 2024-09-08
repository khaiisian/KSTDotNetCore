

Scaffold-DbContext "Server=.;Database=DotNetTrainingBatch4;User ID=sa;Password=sa@123;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Context AppDbContext


dotnet ef dbcontext scaffold "Server=.;Database=DotNetTrainingBatch4;User Id=sa;Password=sa@123;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models -c AppDbContext -f