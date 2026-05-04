sudo apt update && sudo apt -y upgrade
sudo apt install -y nginx
sudo systemctl start nginx
sudo systemctl enable nginx
curl -fsSL https://deb.nodesource.com/setup_20.x | sudo -E bash -
sudo apt install -y nodejs
node -v
npm -v
git clone https://github.com/lovelu99/react-asp.net-core-sql.git
cd react-asp.net-core-sql/
cd frontend/
npm install
npm run build

sudo vi /etc/nginx/sites-available/default


server {
    listen 80;
    server_name _;

    root /var/www/frontend;
    index index.html;

    location / {
        try_files $uri /index.html;
    }

    location /api/ {
        proxy_pass http://10.0.2.4:5000;
    }
}


sudo mkdir -p /var/www/frontend
sudo cp -r dist/* /var/www/frontend/
sudo chown -R www-data:www-data /var/www/frontend
sudo systemctl restart nginx


docker buildx build --platform linux/amd64 --provenance=false   -t loveluapi.azurecr.io/frontend:2.0   --push .


##### backend

sudo apt update
sudo apt -y upgrade
sudo apt install -y aspnetcore-runtime-8.0

mkdir -p ~/myapi

dotnet backend.dll

sudo vi /etc/systemd/system/myapi.service


[Unit]
Description=My ASP.NET Core API
After=network.target

[Service]
WorkingDirectory=/home/azureuser/myapi
ExecStart=/usr/bin/dotnet /home/azureuser/myapi/backend.dll
Restart=always
RestartSec=5
KillSignal=SIGINT
SyslogIdentifier=myapi
User=azureuser
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=ASPNETCORE_URLS=http://0.0.0.0:5000

[Install]
WantedBy=multi-user.target



sudo systemctl daemon-reload
sudo systemctl enable myapi
sudo systemctl start myapi
sudo systemctl status myapi

sudo vi /etc/nginx/sites-available/default




sudo apt update
sudo apt install -y wget apt-transport-https

wget https://packages.microsoft.com/config/ubuntu/$(lsb_release -rs)/packages-microsoft-prod.deb

sudo dpkg -i packages-microsoft-prod.deb

sudo apt update

mkdir -p ~/myapi
cd myapi


CREATE TABLE Products (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Price DECIMAL(18,2) NOT NULL
);

INSERT INTO Products (Name, Price)
VALUES ('Laptop', 999.99),
       ('Mouse', 25.50),
       ('Keyboard', 75.00);

       
