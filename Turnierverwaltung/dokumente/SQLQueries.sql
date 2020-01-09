SELECT * FROM Club
SELECT * FROM Game
SELECT * FROM [Group]
SELECT * FROM Tournment
SELECT * FROM [User]


Insert INTO Tournment (Description, StartDate, EndDate) values ('Saison17/18', '2017-06-27', '2018-05-26')
Insert INTO Tournment (Description, StartDate, EndDate) values ('Saison18/19', '2017-06-29', '2018-06-01')

Insert INTO [Group] (Name, TournamentFk) values ('Gruppe A', '1')
Insert INTO [Group] (Name, TournamentFk) values ('Gruppe B', '1')
Insert INTO [Group] (Name, TournamentFk) values ('Gruppe C', '1')
Insert INTO [Group] (Name, TournamentFk) values ('Gruppe D', '1')
Insert INTO [Group] (Name, TournamentFk) values ('Gruppe E', '1')
Insert INTO [Group] (Name, TournamentFk) values ('Gruppe F', '1')
Insert INTO [Group] (Name, TournamentFk) values ('Gruppe G', '1')
Insert INTO [Group] (Name, TournamentFk) values ('Gruppe H', '1')

Insert INTO [Group] (Name, TournamentFk) values ('Gruppe A', '2')
Insert INTO [Group] (Name, TournamentFk) values ('Gruppe B', '2')
Insert INTO [Group] (Name, TournamentFk) values ('Gruppe C', '2')
Insert INTO [Group] (Name, TournamentFk) values ('Gruppe D', '2')
Insert INTO [Group] (Name, TournamentFk) values ('Gruppe E', '2')
Insert INTO [Group] (Name, TournamentFk) values ('Gruppe F', '2')
Insert INTO [Group] (Name, TournamentFk) values ('Gruppe G', '2')
Insert INTO [Group] (Name, TournamentFk) values ('Gruppe H', '2')

Insert INTO Club (Name, GroupFk) values ('Manchester United', '1')
Insert INTO Club (Name, GroupFk) values ('FC Basel', '1')
Insert INTO Club (Name, GroupFk) values ('ZSKA Moskau', '1')
Insert INTO Club (Name, GroupFk) values ('Benfica Lissabon', '1')

Insert INTO Club (Name, GroupFk) values ('Paris Saint-Germain', '2')
Insert INTO Club (Name, GroupFk) values ('FC Bayern München', '2')
Insert INTO Club (Name, GroupFk) values ('Celtic Glasgow', '2')
Insert INTO Club (Name, GroupFk) values ('RSC Anderlecht', '2')

Insert INTO Club (Name, GroupFk) values ('AS Rom', '3')
Insert INTO Club (Name, GroupFk) values ('FC Chelsea', '3')
Insert INTO Club (Name, GroupFk) values ('Atlético Madrid', '3')
Insert INTO Club (Name, GroupFk) values ('Qaraba? A?dam', '3')

Insert INTO Club (Name, GroupFk) values ('FC Barcelona', '4')
Insert INTO Club (Name, GroupFk) values ('Juventus Turin', '4')
Insert INTO Club (Name, GroupFk) values ('Sporting Lissabon', '4')
Insert INTO Club (Name, GroupFk) values ('Olympiakos Piräus', '4')

Insert INTO Club (Name, GroupFk) values ('FC Liverpool', '5')
Insert INTO Club (Name, GroupFk) values ('FC Sevilla', '5')
Insert INTO Club (Name, GroupFk) values ('Spartak Moskau', '5')
Insert INTO Club (Name, GroupFk) values ('NK Maribor', '5')

Insert INTO Club (Name, GroupFk) values ('Manchester City', '6')
Insert INTO Club (Name, GroupFk) values ('Schachtar Donezk', '6')
Insert INTO Club (Name, GroupFk) values ('SSC Neapel', '6')
Insert INTO Club (Name, GroupFk) values ('Feyenoord Rotterdam', '6')

Insert INTO Club (Name, GroupFk) values ('Be?ikta? Istanbul', '7')
Insert INTO Club (Name, GroupFk) values ('FC Porto', '7')
Insert INTO Club (Name, GroupFk) values ('RB Leipzig', '7')
Insert INTO Club (Name, GroupFk) values ('AS Monaco', '7')

Insert INTO Club (Name, GroupFk) values ('Tottenham Hotspur', '8')
Insert INTO Club (Name, GroupFk) values ('Real Madrid', '8')
Insert INTO Club (Name, GroupFk) values ('Borussia Dortmund', '8')
Insert INTO Club (Name, GroupFk) values ('APOEL Nikosia', '8')

Insert INTO Club (Name, GroupFk) values ('Borussia Dortmund', '9')
Insert INTO Club (Name, GroupFk) values ('Atlético Madrid', '9')
Insert INTO Club (Name, GroupFk) values ('FC Brügge', '9')
Insert INTO Club (Name, GroupFk) values ('AS Monaco', '9')

Insert INTO Club (Name, GroupFk) values ('FC Barcelona', '10')
Insert INTO Club (Name, GroupFk) values ('Tottenham Hotspur', '10')
Insert INTO Club (Name, GroupFk) values ('Inter Mailand', '10')
Insert INTO Club (Name, GroupFk) values ('PSV Eindhoven', '10')

Insert INTO Club (Name, GroupFk) values ('Paris Saint-Germain', '11')
Insert INTO Club (Name, GroupFk) values ('FC Liverpool', '11')
Insert INTO Club (Name, GroupFk) values ('SSC Neapel', '11')
Insert INTO Club (Name, GroupFk) values ('FK Roter Stern Belgrad', '11')

Insert INTO Club (Name, GroupFk) values ('FC Proto', '12')
Insert INTO Club (Name, GroupFk) values ('FC Schalke 04', '12')
Insert INTO Club (Name, GroupFk) values ('Galatasaray Istanbul', '12')
Insert INTO Club (Name, GroupFk) values ('Lokomotive Moskau', '12')

Insert INTO Club (Name, GroupFk) values ('FC Bayern München', '13')
Insert INTO Club (Name, GroupFk) values ('Ajax Amsterdam', '13')
Insert INTO Club (Name, GroupFk) values ('Benfica Lissabon', '13')
Insert INTO Club (Name, GroupFk) values ('AEK Athen', '13')

Insert INTO Club (Name, GroupFk) values ('Manchester City', '14')
Insert INTO Club (Name, GroupFk) values ('Olympique Lion', '14')
Insert INTO Club (Name, GroupFk) values ('Schachtar Donezk', '14')
Insert INTO Club (Name, GroupFk) values ('TSG 1899 Hoffenheim', '14')

Insert INTO Club (Name, GroupFk) values ('Real Madrid', '15')
Insert INTO Club (Name, GroupFk) values ('AS Rom', '15')
Insert INTO Club (Name, GroupFk) values ('Viktoria Pilsen', '15')
Insert INTO Club (Name, GroupFk) values ('ZSKA Moskau', '15')

Insert INTO Club (Name, GroupFk) values ('Juventus Turin', '16')
Insert INTO Club (Name, GroupFk) values ('Manchester United', '16')
Insert INTO Club (Name, GroupFk) values ('FC Valencia', '16')
Insert INTO Club (Name, GroupFk) values ('BSC Young Boys', '16')