insert into Users(Name, Email, Password)
values('Everton', 'joao.123@gmail.com', '123')

insert into Address(UserId, AddressLine, Number, Complement, City, State, Country, ZipCode)
values(1, 'Av. José Bonifácio', 2239, 'Ap 159, Bem Te Vs', 'São Paulo', 'SP', 'Brasil', '05438999')

insert into ImageType(Name)
values('ProfilePhoto')

insert into Images(UserId, ImageTypeId, Value)
values(1, 1, 'inserir base 64 aqui')

insert into ProfessionalInformations(UserId, Description)
values(1, 'Trabalho como eletricista há 5 anos. Tenho experiência em instalação e manutenção');

insert into Services(ProfessionalInformationsId, Name)
values(1, 'Eletricista')

INSERT INTO RatingType(Description)
VALUES('Default');

INSERT INTO Ratings(UserId, RatingTypeId, RatingValue)
VALUES(1, 1, 4.5);

INSERT INTO Phones(UserId, DDD, PhoneNumber)
VALUES(1, '11', '964888438');