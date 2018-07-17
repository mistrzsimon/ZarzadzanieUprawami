
drop database zarzadzanieUprawami2

DROP TABLE PraceZasoby
DROP TABLE PracePolowe 
DROP TABLE Zasób
DROP TABLE Producenci
DROP TABLE Magazyn
DROP TABLE RodzajePrac
DROP TABLE Pracownicy
DROP TABLE StanyUpraw
DROP TABLE Uprawy
DROP TABLE Pola
DROP TABLE Rosliny
DROP TABLE StanyPola
DROP TABLE Odmiany
DROP TABLE Gatunki
DROP TABLE Obszary
DROP TABLE Gleby
DROP TABLE Okresowosci


CREATE DATABASE zarzadzanieUprawami2

CREATE TABLE Obszary
(idObszar int PRIMARY KEY not null,
powierzchniaHa float,
szer_geo float,
dl_geo float)


CREATE TABLE Gleby
(idGleba int PRIMARY KEY not null,
rodzaj varchar(25))

CREATE TABLE Pola
(idPole int PRIMARY KEY not null,
idObszar int references Obszary(idObszar),
klasa int,
idGleba int references Gleby(idGleba)
)


CREATE TABLE Okresowosci
(idOkresowosc int PRIMARY KEY not null,
rodzaj varchar(25))

CREATE TABLE Odmiany
(idOdmiana int PRIMARY KEY not null,
nazwa varchar(20))

CREATE TABLE Gatunki
(idGatunek int PRIMARY KEY not null,
nazwa varchar(20))

CREATE TABLE Rosliny
(idRoslina int PRIMARY KEY not null,
idGatunek int references Gatunki(idGatunek),
idOdmiana int references Odmiany(idOdmiana),
idOkresowosc int references Okresowosci(idOkresowosc),
doplatyHa float,
siewKgHa int,
zbiorKgHa float,
wartoscKg float ,
kosztOchronyHa float,
koszNawozuHa float,
kosztZbioruHa float,
)


CREATE TABLE StanyPola
(idStan int PRIMARY KEY not null,
nazwa varchar(20))


CREATE TABLE Uprawy
(idUprawa int PRIMARY KEY not null,
idPole int references Pola(idPole),
idRoslina int references Rosliny(idRoslina),
dataPoczatkowa datetime,
dataKoncowa dateTime)

CREATE TABLE StanyUpraw
(idUprawaStan int PRIMARY KEY not null,
idUprawa int references Uprawy(idUprawa),
idStan int references StanyPola(idStan))

create table Pracownicy
(
idPracownik int primary key not null,
imiê varchar(20),
nazwisko varchar(50)
)
create table RodzajePrac
(
idPraca int primary key not null,
nazwa text
)
create table Magazyn
(
idEwidencja int primary key not null,
idZasób int,
ilosc int
)
create table Producenci
(
idProducent int primary key not null,
nazwa text
)

create Table Zasób
(
idZasób int primary key references Magazyn(idEwidencja) not null,
NazwaZasobu varchar(20),
idProducent int references Producenci(idProducent)
)

create table PracePolowe
(
idPracaPolowa int primary key not null,
idPoleUprawa int references Uprawy(idUprawa),
idWykonawca int references Pracownicy(idPracownik),
idPraca int references RodzajePrac(idPraca),
dataWykonanejPracy Date
)

create table PraceZasoby
(
idPracaZasób int primary key not null,
idPracaPolowa int references PracePolowe(idPracaPolowa),
idZasób int references Zasób(idZasób),
iloœæZu¿ytegoZasobu int
)


INSERT INTO StanyPola(idStan, nazwa) VALUES(1, 'Zaorane')
INSERT INTO StanyPola(idStan, nazwa) VALUES(2, 'Nawo?one')
INSERT INTO StanyPola(idStan, nazwa) VALUES(3, 'Zasiane')
INSERT INTO StanyPola(idStan, nazwa) VALUES(4, 'Opryskane')
INSERT INTO StanyPola(idStan, nazwa) VALUES(5, 'Uprawione')
INSERT INTO StanyPola(idStan, nazwa) VALUES(6, 'DoZbioru')
INSERT INTO StanyPola(idStan, nazwa) VALUES(7, 'Zebrane')
INSERT INTO StanyPola(idStan, nazwa) VALUES (8, 'PoKlesceZywiolowej')

Insert into Okresowosci(idOkresowosc,rodzaj)values(1,'letnia')
Insert into Okresowosci(idOkresowosc,rodzaj)values(2,'wiosenna')
Insert into Okresowosci(idOkresowosc,rodzaj)values(3,'przejsciowa')
Insert into Okresowosci(idOkresowosc,rodzaj)values(4,'jesienna')
Insert into Okresowosci(idOkresowosc,rodzaj)values(5,'zimowa')

INSERT INTO Odmiany(idOdmiana, nazwa) VALUES(1, 'Ozima')
INSERT INTO Odmiany(idOdmiana, nazwa) VALUES(2, 'Jara')
INSERT INTO Odmiany(idOdmiana, nazwa) VALUES(3, 'Wczesna')
INSERT INTO Odmiany(idOdmiana, nazwa) VALUES(4, 'Pozna')
INSERT INTO Odmiany(idOdmiana, nazwa) VALUES(5, 'Wczesna')
INSERT INTO Odmiany(idOdmiana, nazwa) VALUES(6, 'MrozoOdporna')
INSERT INTO Odmiany(idOdmiana, nazwa) VALUES(7, 'Pastewna')
INSERT INTO Odmiany(idOdmiana, nazwa) VALUES (8, 'Konsumpcyjna')
INSERT INTO Odmiany(idOdmiana, nazwa) VALUES (9, 'Inna')

INSERT INTO Gatunki(idGatunek, nazwa) VALUES(1, 'Pszenica')
INSERT INTO Gatunki(idGatunek, nazwa) VALUES(2, 'Zyto')
INSERT INTO Gatunki(idGatunek, nazwa) VALUES(3, 'Jeczmien')
INSERT INTO Gatunki(idGatunek, nazwa) VALUES(4, 'Kukurydza')
INSERT INTO Gatunki(idGatunek, nazwa) VALUES(5, 'Pszenzyto')
INSERT INTO Gatunki(idGatunek, nazwa) VALUES(6, 'Ziemniak')
INSERT INTO Gatunki(idGatunek, nazwa) VALUES(7, 'Burak')
INSERT INTO Gatunki(idGatunek, nazwa) VALUES(8, 'Marchew')
INSERT INTO Gatunki(idGatunek, nazwa) VALUES(9, 'Pietruszka')
INSERT INTO Gatunki(idGatunek, nazwa) VALUES(10, 'Seler')
INSERT INTO Gatunki(idGatunek, nazwa) VALUES(11, 'Cebula')
INSERT INTO Gatunki(idGatunek, nazwa) VALUES(12, 'Czosnek')
INSERT INTO Gatunki(idGatunek, nazwa) VALUES(13, 'Ogorki')
INSERT INTO Gatunki(idGatunek, nazwa) VALUES(14, 'Pomidory')
INSERT INTO Gatunki(idGatunek, nazwa) VALUES(15, 'Kalarepa')
INSERT INTO Gatunki(idGatunek, nazwa) VALUES(16, 'Rzepak')
INSERT INTO Gatunki(idGatunek, nazwa) VALUES(17, 'Owies')
INSERT INTO Gatunki(idGatunek, nazwa) VALUES(18, 'Kapusta')
INSERT INTO Gatunki(idGatunek, nazwa) VALUES(19, 'Salata')
INSERT INTO Gatunki(idGatunek, nazwa) VALUES(20, 'Rzodkiewka')
INSERT INTO Gatunki(idGatunek, nazwa) VALUES(21, 'Kalafior')

INSERT INTO Gleby(idGleba, rodzaj) VALUES(1, 'Czarnoziemy')
INSERT INTO Gleby(idGleba, rodzaj) VALUES(2, 'Bielicowe')
INSERT INTO Gleby(idGleba, rodzaj) VALUES(3, 'Czarne')
INSERT INTO Gleby(idGleba, rodzaj) VALUES(4, 'Brunatne')
INSERT INTO Gleby(idGleba, rodzaj) VALUES(5, 'Mady')
INSERT INTO Gleby(idGleba, rodzaj) VALUES(6, 'Redziny')
INSERT INTO Gleby(idGleba, rodzaj) VALUES(7, 'Piaski')




INSERT INTO Obszary(idObszar, powierzchniaHa, szer_geo, dl_geo) VALUES(1, 1.3, 50.687013, 17.83123)
INSERT INTO Obszary(idObszar, powierzchniaHa, szer_geo, dl_geo) VALUES(2, 5.2, 50.684033, 17.83228)
INSERT INTO Obszary(idObszar, powierzchniaHa, szer_geo, dl_geo) VALUES(3, 12.3, 50.787012, 17.63123)
INSERT INTO Obszary(idObszar, powierzchniaHa, szer_geo, dl_geo) VALUES(4, 3.1, 49.987023, 17.83423)
INSERT INTO Obszary(idObszar, powierzchniaHa, szer_geo, dl_geo) VALUES(5, 5.5, 50.687013, 17.83123)
INSERT INTO Obszary(idObszar, powierzchniaHa, szer_geo, dl_geo) VALUES(6, 30.2, 50.687013, 17.83123)
INSERT INTO Obszary(idObszar, powierzchniaHa, szer_geo, dl_geo) VALUES(7, 17.2, 50.687013, 17.83123)
INSERT INTO Obszary(idObszar, powierzchniaHa, szer_geo, dl_geo) VALUES(8, 3.2, 50.687013, 17.83123)
INSERT INTO Obszary(idObszar, powierzchniaHa, szer_geo, dl_geo) VALUES(9, 0.6, 50.687013, 17.83123)
INSERT INTO Obszary(idObszar, powierzchniaHa, szer_geo, dl_geo) VALUES(10, 4.2, 50.687013, 17.83123)
INSERT INTO Obszary(idObszar, powierzchniaHa, szer_geo, dl_geo) VALUES(11, 2.1, 50.687013, 17.83123)
INSERT INTO Obszary(idObszar, powierzchniaHa, szer_geo, dl_geo) VALUES(12, 11.2, 50.687013, 17.83123)
INSERT INTO Obszary(idObszar, powierzchniaHa, szer_geo, dl_geo) VALUES(13, 2.2, 50.687013, 17.83123)
INSERT INTO Obszary(idObszar, powierzchniaHa, szer_geo, dl_geo) VALUES(14, 1.4, 50.687013, 17.83123)
INSERT INTO Obszary(idObszar, powierzchniaHa, szer_geo, dl_geo) VALUES(15, 6.7, 50.687013, 17.83123)
INSERT INTO Obszary(idObszar, powierzchniaHa, szer_geo, dl_geo) VALUES(16, 1.3, 50.687013, 17.83123)
INSERT INTO Obszary(idObszar, powierzchniaHa, szer_geo, dl_geo) VALUES(17, 1.3, 50.687013, 17.83123)
INSERT INTO Obszary(idObszar, powierzchniaHa, szer_geo, dl_geo) VALUES(18, 11.3, 50.687013, 17.83123)
INSERT INTO Obszary(idObszar, powierzchniaHa, szer_geo, dl_geo) VALUES(19, 21.3, 50.687013, 17.83123)
INSERT INTO Obszary(idObszar, powierzchniaHa, szer_geo, dl_geo) VALUES(20, 1.3, 50.687013, 17.83123)


INSERT INTO Pola(idPole, idObszar, klasa, idGleba) VALUES(1, 1, 1, 2)
INSERT INTO Pola(idPole, idObszar, klasa, idGleba) VALUES(2, 2, 3, 4)
INSERT INTO Pola(idPole, idObszar, klasa, idGleba) VALUES(3, 3, 1, 3)
INSERT INTO Pola(idPole, idObszar, klasa, idGleba) VALUES(4, 4, 1, 1)
INSERT INTO Pola(idPole, idObszar, klasa, idGleba) VALUES(5, 5, 5, 2)
INSERT INTO Pola(idPole, idObszar, klasa, idGleba) VALUES(6, 6, 2, 4)
INSERT INTO Pola(idPole, idObszar, klasa, idGleba) VALUES(7, 7, 6, 3)
INSERT INTO Pola(idPole, idObszar, klasa, idGleba) VALUES(8, 8, 2, 1)
INSERT INTO Pola(idPole, idObszar, klasa, idGleba) VALUES(9, 9, 5, 2)
INSERT INTO Pola(idPole, idObszar, klasa, idGleba) VALUES(10, 10, 3, 2)
INSERT INTO Pola(idPole, idObszar, klasa, idGleba) VALUES(11, 11, 6, 7)

INSERT [dbo].[Rosliny] ([idRoslina], [idGatunek], [idOdmiana], [idOkresowosc], [doplatyHa], [siewKgHa], [zbiorKgHa], [wartoscKg], [kosztOchronyHa], [koszNawozuHa], [kosztZbioruHa]) VALUES (1, 1, 1, 1, 980, 250, 8000, 0.62, 300, 200, 800)
INSERT [dbo].[Rosliny] ([idRoslina], [idGatunek], [idOdmiana], [idOkresowosc], [doplatyHa], [siewKgHa], [zbiorKgHa], [wartoscKg], [kosztOchronyHa], [koszNawozuHa], [kosztZbioruHa]) VALUES (2, 2, 3, 2, 1300, 200, 6200, 0.45, 200, 200, 800)
INSERT [dbo].[Rosliny] ([idRoslina], [idGatunek], [idOdmiana], [idOkresowosc], [doplatyHa], [siewKgHa], [zbiorKgHa], [wartoscKg], [kosztOchronyHa], [koszNawozuHa], [kosztZbioruHa]) VALUES (3, 3, 1, 3, 1100, 180, 6000, 0.56, 300, 200, 800)
INSERT [dbo].[Rosliny] ([idRoslina], [idGatunek], [idOdmiana], [idOkresowosc], [doplatyHa], [siewKgHa], [zbiorKgHa], [wartoscKg], [kosztOchronyHa], [koszNawozuHa], [kosztZbioruHa]) VALUES (4, 4, 1, 2, 600, 250, 11000, 0.6, 300, 200, 950)
INSERT [dbo].[Rosliny] ([idRoslina], [idGatunek], [idOdmiana], [idOkresowosc], [doplatyHa], [siewKgHa], [zbiorKgHa], [wartoscKg], [kosztOchronyHa], [koszNawozuHa], [kosztZbioruHa]) VALUES (5, 5, 5, 3, 600, 250, 1100, 0.6, 300, 200, 600)
INSERT [dbo].[Rosliny] ([idRoslina], [idGatunek], [idOdmiana], [idOkresowosc], [doplatyHa], [siewKgHa], [zbiorKgHa], [wartoscKg], [kosztOchronyHa], [koszNawozuHa], [kosztZbioruHa]) VALUES (6, 6, 2, 3, 600, 2000, 20000, 0.3, 1000, 500, 1500)
INSERT [dbo].[Rosliny] ([idRoslina], [idGatunek], [idOdmiana], [idOkresowosc], [doplatyHa], [siewKgHa], [zbiorKgHa], [wartoscKg], [kosztOchronyHa], [koszNawozuHa], [kosztZbioruHa]) VALUES (7, 1, 6, 1, 2400, 250, 60000, 0.112, 1300, 1500, 2200)
INSERT [dbo].[Rosliny] ([idRoslina], [idGatunek], [idOdmiana], [idOkresowosc], [doplatyHa], [siewKgHa], [zbiorKgHa], [wartoscKg], [kosztOchronyHa], [koszNawozuHa], [kosztZbioruHa]) VALUES (8, 8, NULL, NULL, 900, 300, 70000, 0.3, 2500, 1200, 9000)
INSERT [dbo].[Rosliny] ([idRoslina], [idGatunek], [idOdmiana], [idOkresowosc], [doplatyHa], [siewKgHa], [zbiorKgHa], [wartoscKg], [kosztOchronyHa], [koszNawozuHa], [kosztZbioruHa]) VALUES (9, 9, NULL, NULL, 900, 500, 20000, 1.5, 3000, 1200, 12000)
INSERT [dbo].[Rosliny] ([idRoslina], [idGatunek], [idOdmiana], [idOkresowosc], [doplatyHa], [siewKgHa], [zbiorKgHa], [wartoscKg], [kosztOchronyHa], [koszNawozuHa], [kosztZbioruHa]) VALUES (10, 1, 1, NULL, 500, 1100, 2000, 1.12, 600, 1200, 1400)
INSERT [dbo].[Rosliny] ([idRoslina], [idGatunek], [idOdmiana], [idOkresowosc], [doplatyHa], [siewKgHa], [zbiorKgHa], [wartoscKg], [kosztOchronyHa], [koszNawozuHa], [kosztZbioruHa]) VALUES (11, 2, 3, NULL, 400, 250, 30000, 0.5, 400, 1600, 3000)
INSERT [dbo].[Rosliny] ([idRoslina], [idGatunek], [idOdmiana], [idOkresowosc], [doplatyHa], [siewKgHa], [zbiorKgHa], [wartoscKg], [kosztOchronyHa], [koszNawozuHa], [kosztZbioruHa]) VALUES (12, 2, 3, NULL, 600, 1100, 2000, 1.12, 600, 1200, 1400)
INSERT [dbo].[Rosliny] ([idRoslina], [idGatunek], [idOdmiana], [idOkresowosc], [doplatyHa], [siewKgHa], [zbiorKgHa], [wartoscKg], [kosztOchronyHa], [koszNawozuHa], [kosztZbioruHa]) VALUES (13, 3, 1, NULL, 600, 1100, 2000, 1.12, 600, 1200, 1400)
INSERT [dbo].[Rosliny] ([idRoslina], [idGatunek], [idOdmiana], [idOkresowosc], [doplatyHa], [siewKgHa], [zbiorKgHa], [wartoscKg], [kosztOchronyHa], [koszNawozuHa], [kosztZbioruHa]) VALUES (14, 4, 1, NULL, 600, 1100, 2000, 1.12, 600, 1200, 1400)
INSERT [dbo].[Rosliny] ([idRoslina], [idGatunek], [idOdmiana], [idOkresowosc], [doplatyHa], [siewKgHa], [zbiorKgHa], [wartoscKg], [kosztOchronyHa], [koszNawozuHa], [kosztZbioruHa]) VALUES (15, 5, 5, NULL, 600, 1100, 2000, 1.12, 600, 1200, 1400)
INSERT [dbo].[Rosliny] ([idRoslina], [idGatunek], [idOdmiana], [idOkresowosc], [doplatyHa], [siewKgHa], [zbiorKgHa], [wartoscKg], [kosztOchronyHa], [koszNawozuHa], [kosztZbioruHa]) VALUES (16, 16, 2, NULL, 900, 280, 3500, 1.27, 600, 1200, 1400)
INSERT [dbo].[Rosliny] ([idRoslina], [idGatunek], [idOdmiana], [idOkresowosc], [doplatyHa], [siewKgHa], [zbiorKgHa], [wartoscKg], [kosztOchronyHa], [koszNawozuHa], [kosztZbioruHa]) VALUES (17, 7, 6, NULL, 600, 280, 6000, 1.12, 500, 1300, 1000)
INSERT [dbo].[Rosliny] ([idRoslina], [idGatunek], [idOdmiana], [idOkresowosc], [doplatyHa], [siewKgHa], [zbiorKgHa], [wartoscKg], [kosztOchronyHa], [koszNawozuHa], [kosztZbioruHa]) VALUES (18, 8, NULL, NULL, 600, 280, 6000, 1.12, 500, 1300, 1000)
INSERT [dbo].[Rosliny] ([idRoslina], [idGatunek], [idOdmiana], [idOkresowosc], [doplatyHa], [siewKgHa], [zbiorKgHa], [wartoscKg], [kosztOchronyHa], [koszNawozuHa], [kosztZbioruHa]) VALUES (19, 9, NULL, NULL, 600, 280, 6000, 1.12, 500, 1300, 1000)
INSERT [dbo].[Rosliny] ([idRoslina], [idGatunek], [idOdmiana], [idOkresowosc], [doplatyHa], [siewKgHa], [zbiorKgHa], [wartoscKg], [kosztOchronyHa], [koszNawozuHa], [kosztZbioruHa]) VALUES (20, 9, NULL, NULL, 600, 280, 6000, 1.12, 500, 1300, 1000)


INSERT INTO Uprawy(idUprawa, idPole, idRoslina, dataPoczatkowa, dataKoncowa) VALUES(1, 1, 1, '2016-02-08', '2016-08-08')
INSERT INTO Uprawy(idUprawa,idPole, idRoslina, dataPoczatkowa, dataKoncowa) VALUES(2,  2, 2, '2016-02-08', '2016-08-08')
INSERT INTO Uprawy(idUprawa,idPole, idRoslina, dataPoczatkowa, dataKoncowa) VALUES(3, 3, 3, '2016-02-08', '2016-08-08')
INSERT INTO Uprawy(idUprawa,idPole, idRoslina, dataPoczatkowa, dataKoncowa) VALUES(4, 4, 4, '2016-02-08', '2016-08-08')
INSERT INTO Uprawy(idUprawa,idPole, idRoslina, dataPoczatkowa, dataKoncowa) VALUES(5, 5, 5,  '2016-02-08', '2016-08-08')
INSERT INTO Uprawy(idUprawa,idPole, idRoslina, dataPoczatkowa, dataKoncowa) VALUES(6,  6, 6, '2016-04-08', '2016-08-08')
INSERT INTO Uprawy(idUprawa,idPole, idRoslina, dataPoczatkowa, dataKoncowa) VALUES(7, 7, 7,  '2016-02-08', '2016-08-08')
INSERT INTO Uprawy(idUprawa,idPole, idRoslina, dataPoczatkowa, dataKoncowa) VALUES(8, 8, 8, '2016-02-08', '2016-08-08')
INSERT INTO Uprawy(idUprawa,idPole, idRoslina, dataPoczatkowa, dataKoncowa) VALUES(9, 9, 9, '2016-02-08', '2016-08-08')




insert into Pracownicy(idPracownik,imiê,nazwisko)values(1,'Donald','trump')
insert into Pracownicy(idPracownik,imiê,nazwisko)values(2,'max','kolanko')
insert into Pracownicy(idPracownik,imiê,nazwisko)values(3,'kaczor','tusk')
insert into Pracownicy(idPracownik,imiê,nazwisko)values(4,'ewa','szydelo')
insert into Pracownicy(idPracownik,imiê,nazwisko)values(5,'ewa','braun')

insert into RodzajePrac(idPraca,nazwa)values(1,'siew')
insert into RodzajePrac(idPraca,nazwa)values(2,'plew')
insert into RodzajePrac(idPraca,nazwa)values(3,'siew')
insert into RodzajePrac(idPraca,nazwa)values(5,'zbior')
insert into RodzajePrac(idPraca,nazwa)values(4,'magazynowanie')


insert into PracePolowe(idPracaPolowa,idPoleUprawa,idWykonawca,idPraca,dataWykonanejPracy)
values(1,1,1,1,'2016-10-08')
insert into PracePolowe(idPracaPolowa,idPoleUprawa,idWykonawca,idPraca,dataWykonanejPracy)
values(2,2,2,2,'2016-1-04')
insert into PracePolowe(idPracaPolowa,idPoleUprawa,idWykonawca,idPraca,dataWykonanejPracy)
values(3,3,3,3,'2016-12-21')
insert into PracePolowe(idPracaPolowa,idPoleUprawa,idWykonawca,idPraca,dataWykonanejPracy)
values(4,4,4,4,'2016-04-12')
insert into PracePolowe(idPracaPolowa,idPoleUprawa,idWykonawca,idPraca,dataWykonanejPracy)
values(5,5,5,5,'2016-1-08')

insert into Producenci(idProducent,nazwa)values(1,'turbo-praca')
insert into Producenci(idProducent,nazwa)values(2,'warBud')
insert into Producenci(idProducent,nazwa)values(3,'MaxYmylix')
insert into Producenci(idProducent,nazwa)values(4,'Siewiex')
insert into Producenci(idProducent,nazwa)values(5,'Traktox')

insert into Magazyn(idEwidencja,idZasób,ilosc)Values(1,1,20)
insert into Magazyn(idEwidencja,idZasób,ilosc)Values(2,3,1000)
insert into Magazyn(idEwidencja,idZasób,ilosc)Values(3,5,200)
insert into Magazyn(idEwidencja,idZasób,ilosc)Values(4,2,33)
insert into Magazyn(idEwidencja,idZasób,ilosc)Values(5,10,10000)
insert into Magazyn(idEwidencja,idZasób,ilosc)Values(6,4,1000)


insert into Zasób(idZasób,NazwaZasobu,idProducent)values(1,'ziemia',1)
insert into Zasób(idZasób,NazwaZasobu,idProducent)values(2,'piach',1)
insert into Zasób(idZasób,NazwaZasobu,idProducent)values(3,'wapno',1)
insert into Zasób(idZasób,NazwaZasobu,idProducent)values(4,'azot',1)
insert into Zasób(idZasób,NazwaZasobu,idProducent)values(5,'siarczany',1)
insert into Zasób(idZasób,NazwaZasobu,idProducent)values(6,'nawozy',1)

insert into PraceZasoby(idPracaZasób,idPracaPolowa,idZasób,iloœæZu¿ytegoZasobu)
values(1,1,1,1000);
insert into PraceZasoby(idPracaZasób,idPracaPolowa,idZasób,iloœæZu¿ytegoZasobu)
values(2,2,2,555);
insert into PraceZasoby(idPracaZasób,idPracaPolowa,idZasób,iloœæZu¿ytegoZasobu)
values(3,3,3,100);
insert into PraceZasoby(idPracaZasób,idPracaPolowa,idZasób,iloœæZu¿ytegoZasobu)
values(4,4,4,10);
insert into PraceZasoby(idPracaZasób,idPracaPolowa,idZasób,iloœæZu¿ytegoZasobu)
values(5,5,5,220);



--------------------------------------------------------------

--------------------------------------------------------------


DELETE Uprawy
WHERE idUprawa=8;


UPDATE Uprawy
SET dataPoczatkowa='2016-05-08',dataKoncowa='2016-10-08'
WHERE idUprawa=9;

Update Rosliny
set idGatunek =1
where idOkresowosc = 1;

insert into gatunki(idGatunek,nazwa)values(22,'owoc')


Select * from PraceZasoby
select * from Zasób
select * from PracePolowe 
select * from Pracownicy
select * from Producenci
select *from Magazyn
select * from RodzajePrac
select* from StanyUpraw
SELECT * FROM StanyPola
SELECT * FROM Odmiany
SELECT * FROM Obszary
SELECT * FROM Pola
select* from gleby
select * from Okresowosci
select * from gatunki
select * from rosliny
SELECT * FROM Uprawy






