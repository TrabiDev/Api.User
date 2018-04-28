insert into Users(Name, Email, DDD, Phone, Password)
values('Everton', 'joao.123@gmail.com', '11', '945679090', '123')

insert into Address(UserId, AddressLine, Number, Complement, City, State, Country, ZipCode)
values(1, 'Av. José Bonifácio', 2239, 'Ap 159, Bem Te Vs', 'São Paulo', 'SP', 'Brasil', '05438999')

insert into ImagesType(Name)
values('ProfilePhoto')

insert into Images(UserId, ImagesTypeId, Value)
values(1, 1, 'inserir base 64 aqui')

insert into ProfessionalInformations(UserId, Description)
values(1, 'Trabalho como eletricista há 5 anos. Tenho experiência em instalação e manutenção');

insert into Services(ProfessionalInformationsId, Name)
values(1, 'Eletricista')