print '' print'*** dropping the database tcg_db ***'
GO
IF EXISTS(SELECT 1 FROM master.dbo.sysdatabases WHERE name = 'tcg_db')
BEGIN
	DROP DATABASE [tcg_db]
END
GO

print '' print'*** creating the database tcg_db'
GO
CREATE DATABASE [tcg_db]
GO

print '' print'*** using the database tcg_db'
GO
USE [tcg_db]
GO

/*
Used to store roles for the users
*/
PRINT '' PRINT '*** creating Role table in tcg_db'
GO
CREATE TABLE [dbo].[Role](
	[RoleID]				[INT]				NOT NULL	IDENTITY(1,1),
	[Name]					[NVARCHAR](50)		NOT NULL,
	[Description]			[NVARCHAR](250)		NOT NULL	DEFAULT '',
	
	CONSTRAINT [pk_role_roleid] PRIMARY KEY ([RoleID] ASC),
	CONSTRAINT [ak_role_name] UNIQUE ([Name] ASC)
)
GO

/*
Used to store user information
connects to roles to check what the user can do
*/
PRINT '' PRINT '*** creating Users table in tcg_db'
GO
CREATE TABLE [dbo].[Users](
	[UserID]				[INT]				NOT NULL	IDENTITY(10000,1),
	[RoleID]				[INT]				NOT NULL,
	[GivenName]				[NVARCHAR](50)		NOT NULL,
	[Surname]				[NVARCHAR](100)		NOT NULL,
	[PasswordHash]			[NVARCHAR](100)		NOT NULL	DEFAULT '9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e',
	[Email]					[NVARCHAR](250)		NOT NULL,
	[PhoneNumber]			[NVARCHAR](11)		NOT NULL,
	[Active]				[BIT]				NOT NULL 	DEFAULT 1,
	
	CONSTRAINT [pk_users_userid] PRIMARY KEY ([UserID] ASC),
	CONSTRAINT [fk_users_roleid] FOREIGN KEY ([RoleID]) REFERENCES [Role]([RoleID]),
	CONSTRAINT [ak_users_email] UNIQUE ([Email] ASC)
)
GO

/*
Used to store alternate art information
E.X.
Name = "reverse holo"
Description = "Standard card with the background of the card holographic."
*/
PRINT '' PRINT '*** creating AlternateArt table in tcg_db'
GO
CREATE TABLE [dbo].[AlternateArt](
	[AlternateArtID]		[INT]				NOT NULL	IDENTITY(1,1),
	[Name]					[NVARCHAR](50)		NOT NULL,
	[Description]			[NVARCHAR](250)		NOT NULL	DEFAULT '',
	
	CONSTRAINT [pk_alternateart_alternateartid] PRIMARY KEY ([AlternateArtID] ASC),
	CONSTRAINT [ak_alternateart_name] UNIQUE ([Name] ASC)
)
Go

/*
Used to store Artist for the PokemonCard table
*/
PRINT '' PRINT '*** creating Artist table in tcg_db'
GO
CREATE TABLE [dbo].[Artist](
	[ArtistID]				[INT]				NOT NULL	IDENTITY(1,1),
	[GivenName]				[NVARCHAR](50)		NOT NULL,
	[Surname]				[NVARCHAR](100)		NULL,
	
	CONSTRAINT [pk_artist_artistid] PRIMARY KEY ([ArtistID] ASC),
	CONSTRAINT [ak_artist_givenname_surname] UNIQUE ([GivenName],[Surname])
)
Go

/*
Used to store Ability for the PokemonCard table
When a trainer/support/item needs an ability
Name = trainer/support/item
otherwise 
Name = ability name from card
*/
PRINT '' PRINT '*** creating Ability table in tcg_db'
GO
CREATE TABLE [dbo].[Ability](
	[AbilityID]				[INT]				NOT NULL	IDENTITY(1,1),
	[Name]					[NVARCHAR](25)		NOT NULL	DEFAULT 'support',
	[Description]			[NVARCHAR](500)		NOT NULL	DEFAULT '',
	
	CONSTRAINT [pk_ability_abilityid] PRIMARY KEY ([AbilityID] ASC),
	CONSTRAINT [ak_ability_description] UNIQUE ([Description])
)
Go

PRINT '' PRINT '*** creating ElementType table in tcg_db'
GO

/*
Needed because PokemonCards/CollectionType/MoveCost can all have more than one
*/
CREATE TABLE [dbo].[ElementType](
	[ElementTypeID]			[INT]				NOT NULL	IDENTITY(1,1),
	[Name]					[NVARCHAR](15)		NOT NULL,
	[Description]			[NVARCHAR](100)		NOT NULL	DEFAULT '',
	
	CONSTRAINT [pk_elementtype_elementtypeid] PRIMARY KEY ([ElementTypeID] ASC),
	CONSTRAINT [ak_elementtype_name] UNIQUE ([Name])
)
Go

/*
Only stores Name, Damage, and Description because
a card can have multiple element types for the cost
*/
PRINT '' PRINT '*** creating Move table in tcg_db'
GO
CREATE TABLE [dbo].[Move](
	[MoveID]				[INT]				NOT NULL	IDENTITY(1,1),
	[Name]					[NVARCHAR](30)		NOT NULL,
	[Damage]				[INT]				NOT NULL,
	[Description]			[NVARCHAR](100)		NOT NULL	DEFAULT '',
	
	CONSTRAINT [pk_move_moveid] PRIMARY KEY ([MoveID] ASC)
)
Go

/*
These are the types of collections a user can CREATE
User's have Favorites, and Wishlist by default
*/
PRINT '' PRINT '*** creating CollectionType table in tcg_db'
GO
CREATE TABLE [dbo].[CollectionType](
	[CollectionTypeID]		[INT]				NOT NULL	IDENTITY(1,1),
	[Name]					[NVARCHAR](25)		NOT NULL,
	[Description]			[NVARCHAR](25)		NOT NULL	DEFAULT '',
	[MaxSize]				[INT]				NOT NULL,	
	
	CONSTRAINT [pk_collectiontype_collectiontypeid] PRIMARY KEY ([CollectionTypeID] ASC),
	CONSTRAINT [ak_collectiontype_name] UNIQUE ([Name] ASC)
)
GO

/*
Collection relates to User so a User can access
any of there collections 
*/
PRINT '' PRINT '*** creating Collection table in tcg_db'
GO
CREATE TABLE [dbo].[Collection](
	[CollectionID]			[INT]				NOT NULL	IDENTITY(1,1),
	[UserID]				[INT]				NOT NULL,
	[CollectionTypeID]		[INT]				NOT NULL,
	[Name]					[NVARCHAR](50)		NOT NULL,
	[Description]			[NVARCHAR](50)		NOT NULL	DEFAULT '',
	[Active]				[BIT]				NOT NULL	DEFAULT 1,
	
	CONSTRAINT [pk_collection_collectionid] PRIMARY KEY ([CollectionID] ASC),
	CONSTRAINT [fk_collection_userid] FOREIGN KEY ([UserID]) REFERENCES [Users] ([UserID]),
	CONSTRAINT [fk_collection_collectiontypeid] FOREIGN KEY ([CollectionTypeID]) REFERENCES [CollectionType] ([CollectionTypeID])
)
GO

/*
Joins Collection and ElementType
because a deck can have more than one element
*/
PRINT '' PRINT '*** creating CollectionElement table in tcg_db'
GO
CREATE TABLE [dbo].[CollectionElement](
	[CollectionElementID]	[INT]				NOT NULL	IDENTITY(1,1),
	[CollectionID]		 	[INT]				NOT NULL,
	[ElementTypeID]		 	[INT]				NOT NULL,
	
	CONSTRAINT [pk_collectiontype_collectionelementid] PRIMARY KEY ([CollectionElementID] ASC),
	CONSTRAINT [fk_collectiontype_collectionid] FOREIGN KEY ([CollectionID]) REFERENCES [Collection]([CollectionID]),
	CONSTRAINT [fk_collectiontype_elementtypeid] FOREIGN KEY ([ElementTypeID]) REFERENCES [ElementType]([ElementTypeID])
)
GO

/*
Pokemon rules are rules specified on the card
EX.
Name = "V Rule"
Description = "When your Pokemon V is Knocked Out, your opponent takes 2 Prize Cards."
*/
PRINT ''PRINT ''PRINT '' PRINT '*** creating PokemonRule table in tcg_db'
GO
CREATE TABLE [dbo].[PokemonRule](
	[PokemonRuleID]			[INT]				NOT NULL	IDENTITY(1,1),
	[Name]					[NVARCHAR](50)		NOT NULL,
	[Description]			[NVARCHAR](100)		NOT NULL	DEFAULT '',
	
	CONSTRAINT [pk_pokemonrule_pokemonruleid] PRIMARY KEY ([PokemonRuleID] ASC),
	CONSTRAINT [ak_pokemonrule_name] UNIQUE ([Name] ASC),
	CONSTRAINT [ak_pokemonrule_description] UNIQUE ([Description])
)
GO

/*
All pokemon
Used with PokedexEvolution to find what 
a card's pokemon can evolve into or from
*/
PRINT ''PRINT ''PRINT '' PRINT '*** creating Pokedex table in tcg_db'
GO
CREATE TABLE [dbo].[Pokedex](
	[PokedexID]				[INT]	 			NOT NULL,
	[Name]					[NVARCHAR](30)		NOT NULL,
	
	CONSTRAINT [pk_pokedex_pokedexid] PRIMARY KEY ([PokedexID] ASC),
	CONSTRAINT [ak_pokedex_name] UNIQUE ([Name])
)
GO

/*
Used with StageEvolution to find what is 
the next or previous stage of a card
*/
PRINT ''PRINT ''PRINT '' PRINT '*** creating Stage table in tcg_db'
GO
CREATE TABLE [dbo].[Stage](
	[StageID]				[INT]				NOT NULL	IDENTITY(1,1),
	[Name]					[INT]               NOT NULL,
	
	CONSTRAINT [pk_stage_stageid] PRIMARY KEY ([StageID]),
	CONSTRAINT [ak_stage_name] UNIQUE ([Name])
)
GO

/*
Used to store the data about the booster packs or sets
*/
PRINT ''PRINT ''PRINT '' PRINT '*** creating Booster table in tcg_db'
GO
CREATE TABLE [dbo].[Booster](
	[BoosterID]				[INT]				NOT NULL 	IDENTITY(1,1),
	[Series]				[NVARCHAR](50)		NOT NULL,
	[BoosterName]			[NVARCHAR](50)		NOT NULL,
	[ReleaseDate]			[DATE]				NOT NULL,
	[Abbreviation]			[NVARCHAR](4)		NOT NULL,
	
	CONSTRAINT [pk_booster_boosterid] PRIMARY KEY ([BoosterID]),
	CONSTRAINT [ak_booster_boostername] UNIQUE ([BoosterName]),
	CONSTRAINT [ak_booster_releasedate] UNIQUE ([ReleaseDate] DESC),
	CONSTRAINT [ak_booster_abbreviation] UNIQUE ([Abbreviation])
)
GO


/*
Used to store all data about the pokemon card
*/
PRINT '' PRINT '*** creating PokemonCard table in tcg_db'
GO
CREATE TABLE [dbo].[PokemonCard](
	[PokemonCardID]			[INT]				NOT NULL	IDENTITY(1,1),
	[AlternateArtID]		[INT]	            NOT NULL,	
	[ArtistID]				[INT]	            NOT NULL,	
	[AbilityID]				[INT]	            NOT NULL,	
	[BoosterID]				[INT]	            NOT NULL,	
	[StageID]				[INT]	            NOT NULL,
	[BoosterNumber]         [INT]               NOT NULL,	
	[Name]					[NVARCHAR](50)	    NOT NULL,	
	[CardType]				[NVARCHAR](50)    	NOT NULL,
	[Rarity]				[NVARCHAR](20)		NOT NULL,
	[Description]           [NVARCHAR](100)     NOT NULL	DEFAULT '',
	[WeaknessType]			[NVARCHAR](15)		NULL,
	[ResistanceType]        [NVARCHAR](15)      NULL,
	[WeaknessValue]         [INT]               NULL,
	[ResistanceValue]       [INT]               NULL,
	[RetreatCost]           [INT]               NULL,
	[Health]				[INT]				NULL,

	
	/*AlternateArtID, boosterid,BoosterID unique*/
	CONSTRAINT [pk_pokemoncard_pokemoncardid] PRIMARY KEY ([PokemonCardID] ASC),
	CONSTRAINT [fk_pokemoncard_alternateid] FOREIGN KEY ([AlternateArtID]) REFERENCES [AlternateArt] ([AlternateArtID]),
	CONSTRAINT [fk_pokemoncard_artistid] FOREIGN KEY ([ArtistID]) REFERENCES [Artist] ([ArtistID]),
	CONSTRAINT [fk_pokemoncard_abilityid] FOREIGN KEY ([AbilityID]) REFERENCES [Ability] ([AbilityID]),
	CONSTRAINT [fk_pokemoncard_boosterid] FOREIGN KEY ([BoosterID]) REFERENCES [Booster] ([BoosterID]),
	CONSTRAINT [fk_pokemoncard_stageid] FOREIGN KEY ([StageID]) REFERENCES [Stage] ([StageID]),
	CONSTRAINT [ak_pokemoncard_alternateid_boosterid_boosternumber] UNIQUE ([AlternateArtID],[BoosterID],[BoosterNumber])
)
GO

/*
Joins the user and the pokemon cards
Used to keep track of which card each user has
*/
PRINT '' PRINT '*** creating UserCard table in tcg_db'
GO
CREATE TABLE [dbo].[UserCard](
	[UserCardID]			[INT]				NOT NULL	IDENTITY(1,1),
	[UserID]				[INT]				NOT NULL,
	[PokemonCardID]			[INT]				NOT NULL,
	[Quantity]				[INT]				NOT NULL, /*Quantity prevents duplicate entries*/
	[Active]				[BIT]				NOT NULL	DEFAULT 1, /* If the user sells the card and wants to remove it from there card list*/
	
	CONSTRAINT [pk_usercard_usercardid] PRIMARY KEY ([UserCardID] ASC),
	CONSTRAINT [fk_usercard_userid] FOREIGN KEY ([UserID]) REFERENCES [Users]([UserID]),
	CONSTRAINT [fk_usercard_pokemoncardid] FOREIGN KEY ([PokemonCardID]) REFERENCES [PokemonCard]([PokemonCardID]),
)
GO

/*
Used to join the Move and ElementType
Some moves have multiple elements needed to use the move
*/
PRINT '' PRINT '*** creating MoveCost table in tcg_db'
GO
CREATE TABLE [dbo].[MoveCost](
	[MoveCostID]			[INT]				NOT NULL	IDENTITY(1,1),
	[MoveID]				[INT]				NOT NULL,
	[ElementTypeID]			[INT]				NOT NULL,
	[Quantity]				[INT]				NOT NULL,
	
	CONSTRAINT [pk_movecost_movecostid] PRIMARY KEY ([MoveCostID]),
	CONSTRAINT [fk_moveelement_moveid] FOREIGN KEY ([MoveID]) REFERENCES [Move]([MoveID]),
	CONSTRAINT [fk_moveelement_elementtypeid] FOREIGN KEY ([ElementTypeID]) REFERENCES [ElementType]([ElementTypeID]),
)
Go

/*
Used to join PokemonCard and Move
Some cards have more than one move
*/
PRINT '' PRINT '*** creating CardMove table in tcg_db'
GO
CREATE TABLE [dbo].[CardMove](
	[CardMoveID]			[INT]				NOT NULL	IDENTITY(1,1),
	[PokemonCardID]			[INT]				NOT NULL,
	[MoveID]				[INT]				NOT NULL,
	
	CONSTRAINT [pk_cardmove_cardmoveid] PRIMARY KEY ([CardMoveID]),
	CONSTRAINT [fk_cardmove_pokemoncardid] FOREIGN KEY ([PokemonCardID]) REFERENCES [PokemonCard]([PokemonCardID]),
	CONSTRAINT [fk_cardmove_moveid] FOREIGN KEY ([MoveID]) REFERENCES [Move]([MoveID])
)
GO

/*
Used to store the cards in a Collection
This can be used to all collection types(deck,wishlist,ect.)
*/
PRINT '' PRINT '*** creating CollectionList table in tcg_db'
GO
CREATE TABLE [dbo].[CollectionList](
	[CollectionListID]		[INT]				NOT NULL	IDENTITY(1,1),
	[UserCardID]			[INT]				NOT NULL,
	[CollectionID]			[INT]				NOT NULL,
	[Quantity]				[INT]				NOT NULL,
	
	CONSTRAINT [pk_collectionlist_collectionlistid] PRIMARY KEY ([CollectionListID]),
	CONSTRAINT [fk_collectionlist_usercard] FOREIGN KEY ([UserCardID]) REFERENCES [UserCard]([UserCardID]),
	CONSTRAINT [fk_collectionlist_collectionid] FOREIGN KEY ([CollectionID]) REFERENCES [Collection]([CollectionID])
)
GO


/*
Used because some cards have multiple pokemon on them
*/
PRINT ''PRINT ''PRINT '' PRINT '*** creating PokedexCard table in tcg_db'
GO
CREATE TABLE [dbo].[PokedexCard](
	[PokedexCardID]			[INT]				NOT NULL	IDENTITY(1,1),
	[PokedexID]				[INT]				NOT NULL,
	[PokemonCardID]			[INT]				NOT NULL,
	
	CONSTRAINT [pk_pokdexcard_pokedexcardid] PRIMARY KEY ([PokedexCardID]),
	CONSTRAINT [fk_pokedexcard_pokedexid] FOREIGN KEY([PokedexID]) REFERENCES [Pokedex]([PokedexID]),
	CONSTRAINT [fk_pokedexcard_cardid] FOREIGN KEY([PokemonCardID]) REFERENCES [PokemonCard]([PokemonCardID]),
)
GO

/*
Used to find the pokemon's next evolution
*/
PRINT ''PRINT ''PRINT '' PRINT '*** creating PokemonEvolution table in tcg_db'
GO
CREATE TABLE [dbo].[PokedexEvolution](
	[PokedexEvolutionID]	[INT]				NOT NULL	IDENTITY(1,1),
	[CurrentPokedexID]		[INT]				NOT NULL,
	[EvolvedPokedexID]		[INT]				NOT NULL,
	
	CONSTRAINT [pk_pokedexevolution_pokedexevolutionid] PRIMARY KEY ([PokedexEvolutionID]),
	CONSTRAINT [fk_pokedex_currentpokedexid] FOREIGN KEY ([CurrentPokedexID]) REFERENCES [Pokedex]([PokedexID]),
	CONSTRAINT [fk_pokedex_evolvedpokedexid] FOREIGN KEY ([EvolvedPokedexID]) REFERENCES [Pokedex]([PokedexID])
)
GO

/*
Used to find the stage's next evolution
*/
PRINT ''PRINT ''PRINT '' PRINT '*** creating StageEvolution table in tcg_db'
GO
CREATE TABLE [dbo].[StageEvolution](
	[StageEvolutionID]		[INT]				NOT NULL	IDENTITY(1,1),
	[CurrentStageID]		[INT]				NOT NULL,
	[EvolvedStageID]		[INT]				NOT NULL,
	
	CONSTRAINT [pk_stageevolution_stageevolutionid] PRIMARY KEY ([StageEvolutionID]),
	CONSTRAINT [fk_stageevolution_currentstageid] FOREIGN KEY ([CurrentStageID]) REFERENCES [Stage]([StageID]),
	CONSTRAINT [fk_stageevolution_evolvedstageid] FOREIGN KEY ([EvolvedStageID]) REFERENCES [Stage]([StageID])

)
GO


/*
Trigger on UserCard if Quantity is 0 then change bit field to 0

Think of what stored procedures are needed for the program
*/

