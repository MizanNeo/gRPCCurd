## Steps to follow while creating webAPI with gRPC Services

Technologies : .net core, Entityframework, gRPC Services, MS SQL

1) You have to create database in SQL if using sql database.
2) Create database as per your requirements.
3) Create gRPC Service project in .net core.
4) Install required packages Grpc.AspNetCore
	from Nuget package manager or console also intall other required packages like NesoftJson, AutoMapper, Entity Framework

5) do Scaffolding to create table object as model as well as dataset object 

Scaffold-DbContext "Server=your db server;Initial Catalog=your db name;Persist Security Info=False;User ID=your userid;Password=your password;MultipleActiveResultSets=False;Encrypt=True;
			TrustServerCertificate=False;Connection Timeout=30;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir give the path name where you want to do scaffolding


6) Set database connection string in appsettings.json file and some settings in launchsettings.json

 "ConnectionStrings": {
   "DefaultConnection": "Server=DESKTOP-DJP367F;Initial Catalog=EmployeeManagement;Persist Security Info=False;User ID=sa;Password=pass123!@#;TrustServerCertificate=true;MultipleActiveResultSets=true;"
 },
"Kestrel": {
  "EndpointDefaults": {
    "Protocols": "Http2"
  }
}

=>In launchsettings.json

"profiles": {
  "NeoSOFT.GRPCServices": {
    "commandName": "Project",
    "dotnetRunMessages": true,
    "launchBrowser": false,
    //"launchUrl": "swagger",
    "applicationUrl": "https://localhost:7165",
    "environmentVariables": {
      "ASPNETCORE_ENVIRONMENT": "Development"
    }
  }
},


7) Here I used POCO for creating repository instance see in NeoSOFT.Infrastructure-> T4 Templates, do changes in each file as per required. instruction given in each file.

8) Create ".proto" file in gRPC service project it server just like interface


service departMaster{
// Get All method declaration
rpc GellAll(GetAllRequest) returns (GellAllResponse){}
}

//Get All request/response message settings
message GetAllRequest{}
message GellAllResponse{
	repeated GetByIdResponse to_do=1;
}

9) Create .cs class file to override that methods declared in  ".proto" file

 public override async Task<GellAllResponse> GellAll(GetAllRequest request, ServerCallContext context)
 {
     var response = new GellAllResponse();
     var result = await _repository.departMasterRepository.GetAllAsync();

     foreach (var items in result)
     {
         response.ToDo.Add(new GetByIdResponse
         {
             Id=items.Id,
             DepartName=items.DepartName,
             IsActive=items.IsActive

         });
     }
     return await Task.FromResult(response);

 } 

10) Inject dependency in Program.cs for Database connection.
11) Inject dependency bellow the var app= builder.build() in Program.cs file for gRPC Services, such as given bellow.

app.MapGrpcService<departMasterService>();

12) Run the application and check the services I used Postman for checking

In postman for New request-> select gRPC -> use url like localhost:7165/ without https/http 
-> select method if available if not so Import .proto file from your location then select method and provide perameter as per required and click on Invoke.
Click on lock icon to enable TLS
	
