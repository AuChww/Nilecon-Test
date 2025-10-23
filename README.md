# Nilecon-Test
โปรเจคนี้เป็น Web App .Net แบบ MVC ร่วมกับ SQL lite เป็นแบบทดสอบ Nilecon ของ ชนาวุฒิ วุฑฒินันท์

# ติดตั้ง .NET dependencies
```
dotnet restore

# ติดตั้ง Entity Framework CLI tools (ถ้ายังไม่ได้ติดตั้ง)
dotnet tool install --global dotnet-ef

dotnet ef migrations add InitialCreate
dotnet ef database update
```

# ติดตั้ง Tailwind
```
npm install
npm install -D tailwindcss
npx tailwindcss init
npx tailwindcss -i ./wwwroot/css/site.css -o ./wwwroot/css/tailwind.css --watch
```

# Run
```
dotnet build
dotnet run
```