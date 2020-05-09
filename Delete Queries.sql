delete from Published_By where Published_By.Manga_ID = 'Something'

delete from Animes_has_Studio where Animes_has_Studio.Anime_ID = 'Something'

delete from Animes_has_Genres where Animes_has_Genres.Anime_ID = 'Something'
delete from Animes_has_Staff where Animes_has_Staff.Anime_ID = 'Something'
delete from Animes_has_Characters where Animes_has_Characters.Anime_ID = 'Something'
delete from Episodes where Episodes.Anime_ID = 'Something'
delete from dbo.Anime_based_Manga where Anime_based_Manga.Anime_ID = 'Something'
delete from Anime_Sequel where Anime_Sequel.Anime_ID1 = 'Something' or Anime_Sequel.Anime_ID2 = 'Something'

delete from Manga_Sequel where Manga_Sequel.Manga_ID1 = 'Something' or Manga_Sequel.Manga_ID2 = 'Something'
delete from Mangas_has_Characters where Mangas_has_Characters.Manga_ID = 'Something'
delete from Mangas_has_Genres where Mangas_has_Genres.Manga_ID = 'Something'
delete from Mangas_has_Staff where Mangas_has_Staff.Manga_ID = 'Something'
delete from Read_Mangas where Read_Mangas.Manga_ID = 'Something'

delete from Animes_has_Characters where Animes_has_Characters.Character_ID = 'Something'
delete from Mangas_has_Characters where Mangas_has_Characters.Character_ID = 'Something'
delete from Favourite_Characters where Favourite_Characters.Character_ID = 'Something'

delete from Animes_has_Staff where Animes_has_Staff.Staff_ID = 'Something'
delete from Mangas_has_Staff where Mangas_has_Staff.Staff_ID = 'Something'

delete from Users where Users.User_Name = 'Something'
delete from Animes where Animes.Anime_ID  = 'Something'
delete from Characters where Characters.Character_ID  = 'Something'
delete from Staff where staff.Staff_ID  = 'Something'
delete from Mangas where Mangas.Manga_ID  = 'Something'
delete from Publishers where Publishers.Publisher_ID  = 'Something'
delete from Studio where Studio.Studio_ID  = 'Something'



