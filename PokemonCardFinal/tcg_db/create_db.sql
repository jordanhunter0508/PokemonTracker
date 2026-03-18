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

PRINT '' PRINT '' PRINT 'Creating Tables in tcg_db'
/*
Used to store roles for the users
*/
PRINT '*** creating Role Table ***'
GO
CREATE TABLE [dbo].[Role]
(
	[RoleID]				[nvarchar](50)		NOT NULL	DEFAULT 'Unassigned',
	[Description]			[nvarchar](250)		NOT NULL	DEFAULT '',
	
	CONSTRAINT [pk_role_roleid] PRIMARY KEY ([RoleID] ASC)
)
GO

/*
Used to store user information
connects to roles to check what the user can do
*/
PRINT '*** creating Users Table ***'
GO
CREATE TABLE [dbo].[Users]
(
	[UserID]				[int]				NOT NULL	IDENTITY(10000,1),
	[GivenName]				[nvarchar](50)		NOT NULL,
	[Surname]				[nvarchar](100)		NOT NULL,
	[PasswordHash]			[nvarchar](100)		NOT NULL	DEFAULT '9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e',
	[Email]					[nvarchar](250)		NOT NULL,
	[Active]				[bit]				NOT NULL 	DEFAULT 1,
	
	CONSTRAINT [pk_users_userid] PRIMARY KEY ([UserID] ASC),
	CONSTRAINT [ak_users_email] UNIQUE ([Email] ASC)
)
GO

/*
Used so the roleId dose not directly appear in the users table
this can also be used to assign more than one role to a user
*/
PRINT '*** creating UserRole Table ***'
GO
CREATE TABLE [dbo].[UserRole]
(
	[RoleID]		[nvarchar](50)		NOT NULL,
	[UserID]		[int]				NOT NULL
	CONSTRAINT [pk_userrole_userroleid] PRIMARY KEY([UserID], [RoleID]),
	CONSTRAINT [fk_userrole__roleid] FOREIGN KEY([RoleID]) REFERENCES [Role]([RoleID]),
	CONSTRAINT [fk_userrole_userid] FOREIGN KEY ([UserID]) REFERENCES [Users]([UserID])

)
GO

/*
Used to store Ability for the PokemonCard Table
When a trainer/support/item needs an ability
Name = trainer/support/item
otherwise 
Name = ability name from card
*/
PRINT '*** creating Ability Table ***'
GO
CREATE TABLE [dbo].[Ability]
(
	[AbilityID]				[nvarchar](30)		NOT NULL	DEFAULT '',
	[AbilityType]			[nvarchar](25)		NOT NULL	DEFAULT 'support',
	[Description]			[nvarchar](650)		NOT NULL	DEFAULT '',
	[Active]				[bit]				NOT NULL	DEFAULT 1,

	CONSTRAINT [pk_ability_abilityid] PRIMARY KEY ([AbilityID] ASC)
)
GO

/*
Used to store alternate art information
E.X.
Name = "reverse holo"
Description = "Standard card with the background of the card holographic."
*/
PRINT '*** creating AlternateArt Table ***'
GO
CREATE TABLE [dbo].[AlternateArt]
(
	[AlternateArtID]		[nvarchar](50)		NOT NULL,
	[Description]			[nvarchar](250)		NOT NULL	DEFAULT '',
	[Active]				[bit]				NOT NULL	DEFAULT 1,
	
	CONSTRAINT [pk_alternateart_alternateartid] PRIMARY KEY ([AlternateArtID] ASC)
)
GO

/*
Used to store Artist for the PokemonCard Table
*/
PRINT '*** creating Artist Table ***'
GO
CREATE TABLE [dbo].[Artist]
(
	[ArtistID]				[int]				NOT NULL	IDENTITY(1,1),
	[GivenName]				[nvarchar](50)		NOT NULL,
	[Surname]				[nvarchar](100)		NOT NULL	DEFAULT '',
	[Active]				[bit]				NOT NULL	DEFAULT 1,
	
	CONSTRAINT [pk_artist_artistid] PRIMARY KEY ([ArtistID] ASC),
	CONSTRAINT [ak_artist_givenname_surname] UNIQUE ([GivenName],[Surname])
)
GO

/*
Needed because PokemonCards/CollectionType/MoveCost can all have more than one
*/
PRINT '*** creating ElementType Table ***'
GO
CREATE TABLE [dbo].[ElementType]
(
	[ElementTypeID]			[nvarchar](15)		NOT NULL,
	[Description]			[nvarchar](100)		NOT NULL	DEFAULT '',
	
	CONSTRAINT [pk_elementtype_elementtypeid] PRIMARY KEY ([ElementTypeID] ASC)
)
GO

/*
Only stores Name, Damage, and Description because
a card can have multiple element types for the cost
*/
PRINT '*** creating Move Table ***'
GO
CREATE TABLE [dbo].[Move]
(
	[MoveID]				[int]				NOT NULL	IDENTITY(1,1),
	[Name]					[nvarchar](30)		NOT NULL,
	[Damage]				[int]				NOT NULL,
	[Description]			[nvarchar](200)		NOT NULL	DEFAULT '',
	[Active]				[bit]				NOT NULL	DEFAULT 1,
	
	CONSTRAINT [pk_move_moveid] PRIMARY KEY ([MoveID] ASC)
)
GO

/*
These are the types of collections a user can CREATE
User's have Favorites, and Wishlist by default
*/
PRINT '*** creating CollectionType Table ***'
GO
CREATE TABLE [dbo].[CollectionType]
(
	[CollectionTypeID]		[nvarchar](25)		NOT NULL,
	[Description]			[nvarchar](150)		NOT NULL	DEFAULT '',
	[MaxSize]				[int]				NOT NULL,	
	
	CONSTRAINT [pk_collectiontype_collectiontypeid] PRIMARY KEY ([CollectionTypeID] ASC)
)
GO

/*
Collection relates to User so a User can access
any of there collections 
*/
PRINT '*** creating Collection Table ***'
GO
CREATE TABLE [dbo].[Collection]
(
	[CollectionID]			[int]				NOT NULL	IDENTITY(1,1),
	[UserID]				[int]				NOT NULL,
	[CollectionTypeID]		[nvarchar](25)		NOT NULL,
	[Name]					[nvarchar](50)		NOT NULL,
	[Description]			[nvarchar](150)		NOT NULL	DEFAULT '',
	
	CONSTRAINT [pk_collection_collectionid] PRIMARY KEY ([CollectionID] ASC),
	CONSTRAINT [fk_collection_userid] FOREIGN KEY ([UserID]) REFERENCES [Users] ([UserID]),
	CONSTRAINT [fk_collection_collectiontypeid] FOREIGN KEY ([CollectionTypeID]) REFERENCES [CollectionType] ([CollectionTypeID]),
	CONSTRAINT [ak_collection_userID_collectiontypeid_name] UNIQUE ([UserID],[CollectionTypeID],[Name])

)
GO

/*
Joins Collection and ElementType
because a deck can have more than one element
*/
PRINT '*** creating CollectionElement Table ***'
GO
CREATE TABLE [dbo].[CollectionElement]
(
	[CollectionID]		 	[int]				NOT NULL,
	[ElementTypeID]			[nvarchar](15)		NOT NULL,
	
	CONSTRAINT [pk_collectiontype_collectionelementid] PRIMARY KEY ([CollectionID],[ElementTypeID]),
	CONSTRAINT [fk_collectiontype_collectionid] FOREIGN KEY ([CollectionID]) REFERENCES [Collection]([CollectionID]) ON DELETE CASCADE,
	CONSTRAINT [fk_collectiontype_elementtypeid] FOREIGN KEY ([ElementTypeID]) REFERENCES [ElementType]([ElementTypeID])
)
GO

/*
Pokemon rules are rules specified on the card
EX.
Name = "V Rule"
Description = "When your Pokemon V is Knocked Out, your opponent takes 2 Prize Cards."
*/
PRINT '*** creating PokemonRule Table ***'
GO
CREATE TABLE [dbo].[PokemonRule]
(
	[PokemonRuleID]			[nvarchar](50)		NOT NULL,
	[Description]			[nvarchar](150)		NOT NULL,
	
	CONSTRAINT [pk_pokemonrule_pokemonruleid] PRIMARY KEY ([PokemonRuleID] ASC)
)
GO

/*
Used to store the data about the booster packs or sets
*/
PRINT '*** creating Booster Table ***'
GO
CREATE TABLE [dbo].[Booster]
(
	[BoosterID]				[nvarchar](50)		NOT NULL,
	[Series]				[nvarchar](50)		NOT NULL,
	[ReleaseDate]			[date]				NOT NULL,
	[Abbreviation]			[nvarchar](5)		NOT NULL,
	
	CONSTRAINT [pk_booster_boosterid] PRIMARY KEY ([BoosterID]),
	CONSTRAINT [ak_booster_abbreviation] UNIQUE ([Abbreviation])
)
GO


/*
Used to store all data about the pokemon card
*/
PRINT '*** creating PokemonCard Table ***'
GO
CREATE TABLE [dbo].[PokemonCard]
(
	[PokemonCardID]			[int]				NOT NULL	IDENTITY(1,1),
	[ArtistID]				[int]				NOT NULL,	
	[AbilityID]				[nvarchar](30)		NOT NULL,	
	[BoosterID]				[nvarchar](50)		NOT NULL,	
	[PokemonRuleID]			[nvarchar](50)		NOT NULL,
	[ElementTypeID]			[nvarchar](15)		NOT NULL,
	[Name]					[nvarchar](50)	    NOT NULL,
	[BoosterNumber]         [int]				NOT NULL,		
	[CardType]				[nvarchar](50)    	NOT NULL,
	[Rarity]				[nvarchar](30)		NOT NULL,
	[WeaknessType]			[nvarchar](15)		NULL,
	[ResistanceType]        [nvarchar](15)      NULL,
	[WeaknessValue]         [int]               NULL,
	[ResistanceValue]       [int]               NULL,
	[RetreatCost]           [int]               NULL,
	[Health]				[int]				NULL,
	[Stage]					[nvarchar](30)		NOT NULL,

	
	/*AlternateArtID, boosterid,BoosterID unique*/
	CONSTRAINT [pk_pokemoncard_pokemoncardid] PRIMARY KEY ([PokemonCardID] ASC),
	CONSTRAINT [fk_pokemoncard_artistid] FOREIGN KEY ([ArtistID]) REFERENCES [Artist] ([ArtistID]),
	CONSTRAINT [fk_pokemoncard_abilityid] FOREIGN KEY ([AbilityID]) REFERENCES [Ability] ([AbilityID]),
	CONSTRAINT [fk_pokemoncard_boosterid] FOREIGN KEY ([BoosterID]) REFERENCES [Booster] ([BoosterID]),
	CONSTRAINT [fk_pokemoncard_pokemonruleid] FOREIGN KEY ([PokemonRuleID]) REFERENCES [PokemonRule] ([PokemonRuleID]),
	CONSTRAINT [fk_pokemoncard_elementtypeid] FOREIGN KEY ([ElementTypeID]) REFERENCES [ElementType] ([ElementTypeID]),
	CONSTRAINT [ak_pokemoncard_boosterid_boosternumber_rarity] UNIQUE ([BoosterID],[BoosterNumber],[Rarity])
)
GO

/*
Join table between Pokemon Cards and Alternate Art
*/
PRINT '*** creating CardAlternateArt Table ***'
GO
CREATE TABLE [dbo].[CardAlternateArt]
(
	[PokemonCardID]			[int]				NOT NULL,
	[AlternateArtID]		[nvarchar](50)		NOT NULL,
	
	CONSTRAINT [pk_cardalternateart_cardalternateartid] PRIMARY KEY ([PokemonCardID],[AlternateArtID]),
	CONSTRAINT [fk_cardalternateart_pokemoncardid] FOREIGN KEY ([PokemonCardID]) REFERENCES [PokemonCard] ([PokemonCardID]) ON DELETE CASCADE,
	CONSTRAINT [fk_cardalternateart_alternateartid] FOREIGN KEY ([AlternateArtID]) REFERENCES [AlternateArt] ([AlternateArtID]) ON DELETE CASCADE
)
GO

/*
Used to join the Move and ElementType
Some moves have multiple elements needed to use the move
*/
PRINT '*** creating MoveCost Table ***'
GO
CREATE TABLE [dbo].[MoveCost]
(
	[MoveID]				[int]				NOT NULL,
	[ElementTypeID]			[nvarchar](15)		NOT NULL,
	[Quantity]				[int]				NOT NULL,
	
	CONSTRAINT [pk_movecost_movecostid] PRIMARY KEY ([MoveID],[ElementTypeID]),
	CONSTRAINT [fk_moveelement_moveid] FOREIGN KEY ([MoveID]) REFERENCES [Move]([MoveID]) ON DELETE CASCADE,
	CONSTRAINT [fk_moveelement_elementtypeid] FOREIGN KEY ([ElementTypeID]) REFERENCES [ElementType]([ElementTypeID])
)
GO

/*
Used to join PokemonCard and Move
Some cards have more than one move
*/
PRINT '*** creating CardMove Table ***'
GO
CREATE TABLE [dbo].[CardMove]
(
	[PokemonCardID]			[int]				NOT NULL,
	[MoveID]				[int]				NOT NULL,
	
	CONSTRAINT [pk_cardmove_cardmoveid] PRIMARY KEY ([PokemonCardID],[MoveID]),
	CONSTRAINT [fk_cardmove_pokemoncardid] FOREIGN KEY ([PokemonCardID]) REFERENCES [PokemonCard]([PokemonCardID]) ON DELETE CASCADE,
	CONSTRAINT [fk_cardmove_moveid] FOREIGN KEY ([MoveID]) REFERENCES [Move]([MoveID]) ON DELETE CASCADE
)
GO

/*
Used to store the cards in a Collection
This can be used to all collection types(deck,wishlist,ect.)
*/
PRINT '*** creating CollectionList Table ***'
GO
CREATE TABLE [dbo].[CollectionCard]
(
	[CollectionCardID]		[int]				NOT NULL	IDENTITY(1,1),
	[PokemonCardID]			[int]				NOT NULL,
	[CollectionID]			[int]				NOT NULL,
	[Quantity]				[int]				NOT NULL,
	[Owned]					[bit]				NOT NULL	DEFAULT 0,
	
	CONSTRAINT [pk_collectioncard_collectioncardid] PRIMARY KEY ([CollectionCardID]),
	CONSTRAINT [fk_collectioncard_pokemoncardid] FOREIGN KEY ([PokemonCardID]) REFERENCES [PokemonCard]([PokemonCardID]) ON DELETE CASCADE,
	CONSTRAINT [fk_collectioncard_collectionid] FOREIGN KEY ([CollectionID]) REFERENCES [Collection]([CollectionID]) ON DELETE CASCADE,
	CONSTRAINT [ak_collectioncard_cardid_collectionid] UNIQUE ([PokemonCardID],[CollectionID])
)
GO