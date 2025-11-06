/*
Trigger on UserCard if Quantity is 0 then change bit field to 0
Trigger for owned in collection

when card is added to UserCard
and it is in a user collection with the same id as in UserCard
then change owned to 1


Think of what stored procedures are needed for the program

Might want to add a user Role so the user doesn't directly say Admin

Very few cards have more than one ability but could make a join table for them
if there is enough time


Ask Jim about sign up automatically opening view profile

May won't select_booster_by_abbreviation

*/

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
	
	CONSTRAINT [pk_artist_artistid] PRIMARY KEY ([ArtistID] ASC),
	CONSTRAINT [ak_artist_givenname_surname] UNIQUE ([GivenName],[Surname])
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
	[AbilityID]				[nvarchar](30)		NOT NULL	DEFAULT 'support',
	[AbilityType]			[nvarchar](25)		NOT NULL	DEFAULT '',
	[Description]			[nvarchar](650)		NOT NULL	DEFAULT '',

	CONSTRAINT [pk_ability_abilityid] PRIMARY KEY ([AbilityID] ASC)
)
Go

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
Go

/*
Only stores Name, Damage, and Description because
a card can have multiple element types for the cost
*/
PRINT '*** creating Move Table ***'
GO
CREATE TABLE [dbo].[Move]
(
	[MoveID]				[nvarchar](30)		NOT NULL,
	[Damage]				[int]				NOT NULL,
	[Description]			[nvarchar](200)		NOT NULL	DEFAULT '',
	
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
	[Description]			[nvarchar](25)		NOT NULL	DEFAULT '',
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
	[Description]			[nvarchar](50)		NOT NULL	DEFAULT '',
	[Active]				[bit]				NOT NULL	DEFAULT 1,
	
	CONSTRAINT [pk_collection_collectionid] PRIMARY KEY ([CollectionID] ASC),
	CONSTRAINT [fk_collection_userid] FOREIGN KEY ([UserID]) REFERENCES [Users] ([UserID]),
	CONSTRAINT [fk_collection_collectiontypeid] FOREIGN KEY ([CollectionTypeID]) REFERENCES [CollectionType] ([CollectionTypeID])
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
PRINT '*** creating PokemonRule Table ***'
GO
CREATE TABLE [dbo].[PokemonRule]
(
	[PokemonRuleID]			[nvarchar](50)		NOT NULL,
	[Description]			[nvarchar](100)		NOT NULL,
	
	CONSTRAINT [pk_pokemonrule_pokemonruleid] PRIMARY KEY ([PokemonRuleID] ASC),
	CONSTRAINT [ak_pokemonrule_description] UNIQUE ([Description])
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
	[Abbreviation]			[nvarchar](4)		NOT NULL,
	
	CONSTRAINT [pk_booster_boosterid] PRIMARY KEY ([BoosterID]),
	CONSTRAINT [ak_booster_releasedate] UNIQUE ([ReleaseDate] DESC),
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
	[BoosterNumber]         [int]				NOT NULL,	
	[Name]					[nvarchar](50)	    NOT NULL,	
	[CardType]				[nvarchar](50)    	NOT NULL,
	[Rarity]				[nvarchar](20)		NOT NULL,
	[Description]           [nvarchar](100)     NOT NULL	DEFAULT '',
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
	CONSTRAINT [ak_pokemoncard_alternateid_boosterid_boosternumber] UNIQUE ([BoosterID],[BoosterNumber])
)
GO

/*
Join table between Pokemon Cards and Alternate Art
*/
PRINT '*** creating CardAlternateArt Table ***'
GO
CREATE TABLE [dbo].[CardAlternateArt]
(
	[PokemonCardID]			[int]				NOT NULL	IDENTITY(1,1),
	[AlternateArtID]		[nvarchar](50)		NOT NULL,
	
	CONSTRAINT [pk_cardalternateart_cardalternateartid] PRIMARY KEY ([PokemonCardID],[AlternateArtID]),
	CONSTRAINT [fk_cardalternateart_pokemoncardid] FOREIGN KEY ([PokemonCardID]) REFERENCES [PokemonCard] ([PokemonCardID]),
	CONSTRAINT [fk_cardalternateart_alternateartid] FOREIGN KEY ([AlternateArtID]) REFERENCES [AlternateArt] ([AlternateArtID])
)
GO

/*
Joins the user and the pokemon cards
Used to keep track of which card each user has
*/
PRINT '*** creating UserCard Table ***'
GO
CREATE TABLE [dbo].[UserCard]
(
	[UserID]				[int]				NOT NULL,
	[PokemonCardID]			[int]				NOT NULL,
	[Quantity]				[int]				NOT NULL, /*Quantity prevents duplicate entries*/
	[Active]				[bit]				NOT NULL	DEFAULT 1, /* If the user sells the card and wants to remove it from there card list*/
	
	CONSTRAINT [pk_usercard_usercardid] PRIMARY KEY ([UserID], [PokemonCardID]),
	CONSTRAINT [fk_usercard_userid] FOREIGN KEY ([UserID]) REFERENCES [Users]([UserID]),
	CONSTRAINT [fk_usercard_pokemoncardid] FOREIGN KEY ([PokemonCardID]) REFERENCES [PokemonCard]([PokemonCardID]),
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
	[MoveID]				[nvarchar](30)		NOT NULL,
	[ElementTypeID]			[nvarchar](15)		NOT NULL,
	[Quantity]				[int]				NOT NULL,
	
	CONSTRAINT [pk_movecost_movecostid] PRIMARY KEY ([MoveID],[ElementTypeID]),
	CONSTRAINT [fk_moveelement_moveid] FOREIGN KEY ([MoveID]) REFERENCES [Move]([MoveID]),
	CONSTRAINT [fk_moveelement_elementtypeid] FOREIGN KEY ([ElementTypeID]) REFERENCES [ElementType]([ElementTypeID]),
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
	[MoveID]				[nvarchar](30)		NOT NULL,
	
	CONSTRAINT [pk_cardmove_cardmoveid] PRIMARY KEY ([PokemonCardID],[MoveID]),
	CONSTRAINT [fk_cardmove_pokemoncardid] FOREIGN KEY ([PokemonCardID]) REFERENCES [PokemonCard]([PokemonCardID]),
	CONSTRAINT [fk_cardmove_moveid] FOREIGN KEY ([MoveID]) REFERENCES [Move]([MoveID])
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
	
	CONSTRAINT [pk_collectionlist_collectioncardid] PRIMARY KEY ([CollectionCardID]),
	CONSTRAINT [fk_collectionlist_pokemoncardid] FOREIGN KEY ([PokemonCardID]) REFERENCES [PokemonCard]([PokemonCardID]),
	CONSTRAINT [fk_collectionlist_collectionid] FOREIGN KEY ([CollectionID]) REFERENCES [Collection]([CollectionID])
)
GO





PRINT '' PRINT '' PRINT 'Creating Stored Procedures in tcg_db'

PRINT '*** creating sp_authenticate_user_by_email_and_password_hash ***'
GO
CREATE PROCEDURE [dbo].[sp_authenticate_user_by_email_and_password_hash]
	(
		@Email				[nvarchar](250),
		@PasswordHash		[nvarchar](100)
	)
AS
	BEGIN
		SELECT	COUNT([Users].[UserID])
		FROM	[Users]
		WHERE	[Users].[Email] = @Email
			AND	[Users].[PasswordHash] = @PasswordHash
			AND	[Users].[Active] = 1;
	END
GO

PRINT '*** creating sp_select_user_by_email ***'
GO
CREATE PROCEDURE [dbo].[sp_select_user_by_email]
	(
		@Email				[nvarchar](250)
	)
AS
	BEGIN
		SELECT	[Users].[UserID],[Users].[GivenName],[Users].[Surname],
					[Email],[Active]
		FROM	[Users]
		WHERE	[Email] = @Email;
	END
GO

PRINT '*** creating sp_select_role_by_email ***'
GO
CREATE PROCEDURE [dbo].[sp_select_role_by_user_email]
	(
		@Email				[nvarchar](250)
	)
AS
	BEGIN
		SELECT	[UserRole].[RoleID]
		FROM	[UserRole] JOIN [Users] ON [UserRole].[UserID] = [Users].[UserID]
		WHERE	[Users].[Email] = @Email;
	END
GO

PRINT '*** creating sp_insert_user_into_user ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_user_into_user]
	(
		@GivenName				[nvarchar](50),
		@Surname				[nvarchar](100),
		@Email					[nvarchar](250),
		@PasswordHash			[nvarchar](100)		
	)
AS
	BEGIN
		INSERT INTO [dbo].[Users]
			([GivenName],[Surname],[PasswordHash],[Email])
		VALUES
			(@GivenName,@Surname,@PasswordHash,@Email)
		RETURN SCOPE_IDENTITY();
	END
GO

PRINT '*** creating sp_select_user_count_by_email ***'
GO
CREATE PROCEDURE [dbo].[sp_select_user_count_by_email]
	(
		@Email		[nvarchar](250)
	)
AS
	BEGIN
		SELECT	COUNT([Users].[UserID])
		FROM	[Users]
		WHERE	[Email] = @Email;
	END
GO	

PRINT '*** creating sp_insert_user_into_role ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_user_into_role]
	(
		@UserID		[int],
		@RoleID		[nvarchar](50)			
	)
AS
	BEGIN
		INSERT INTO [dbo].[UserRole]
			([RoleID],[UserID])
		VALUES
			(@RoleID,@UserID)
		RETURN @@ROWCOUNT;
	END
GO

PRINT '*** creating sp_update_passwordhash_by_email ***'
GO
CREATE PROCEDURE [dbo].[sp_update_passwordhash_by_email]
	(
		@Email					[nvarchar](250),
		@CurrentPasswordHash	[nvarchar](100),
		@NewPasswordHash		[nvarchar](100)
	)
AS
	BEGIN
		UPDATE 	[Users]
		SET		[Users].[PasswordHash] = @NewPasswordHash
		WHERE	[Users].[PasswordHash] = @CurrentPasswordHash
			AND	@CurrentPasswordHash != @NewPasswordHash
			AND	[Users].[Email] = @Email
		RETURN 	@@ROWCOUNT;
	END
GO

PRINT '*** creating sp_select_move_by_moveid ***'
GO
CREATE PROCEDURE [dbo].[sp_select_move_by_moveid]
	(
		@MoveID			[nvarchar](30)
	)
AS
	BEGIN
		SELECT 	[Move].[MoveID], [Move].[Damage], [Move].[Description]
		FROM	[Move]
		WHERE	[Move].[MoveID] LIKE @MoveID;
	END
GO

PRINT '*** creating sp_select_all_fields_for_move_by_moveid ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_fields_for_move_by_moveid]
	(
		@MoveID			[nvarchar](30)
	)
AS
	BEGIN
		SELECT 	[Move].[MoveID], [Move].[Damage], [Move].[Description],
				[MoveCost].[ElementTypeID],[MoveCost].[Quantity]
		FROM	[Move] JOIN [MoveCost] ON [Move].[MoveID] = [MoveCost].[MoveID]
		WHERE	[Move].[MoveID] LIKE @MoveID;
	END
GO

PRINT '*** creating sp_select_element_by_elementtypeid ***'
GO
CREATE PROCEDURE [dbo].[sp_select_element_by_elementtypeid]
	(
		@ElementTypeID	[nvarchar](15)
	)
AS
	BEGIN
		SELECT 	[ElementType].[ElementTypeID],[ElementType].[Description]
		FROM	[ElementType]
		WHERE	[ElementType].[ElementTypeID] = @ElementTypeID;
	END
GO

PRINT '*** creating sp_insert_element_into_element_type ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_element_into_element_type]
	(
		@ElementTypeID	[nvarchar](15),
		@Description	[nvarchar](100)
	)
AS
	BEGIN
		INSERT INTO [dbo].[ElementType]
			([ElementTypeID],[Description])
		VALUES
			(@ElementTypeID,@Description)
		RETURN @@ROWCOUNT;
	END
GO

PRINT '*** creating sp_update_element_description_by_elementtypeid ***'
GO
CREATE PROCEDURE [dbo].[sp_update_element_description_by_elementtypeid]
	(
		@ElementTypeID	[nvarchar](15),
		@Description	[nvarchar](100)
	)
AS
	BEGIN
		UPDATE 	[ElementType]
		SET		[ElementType].[Description] = @Description
		WHERE	[ElementType].[ElementTypeID] = @ElementTypeID
		RETURN	@@ROWCOUNT;
	END
GO

PRINT '*** creating sp_delete_element_by_elementtypeid ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_element_by_elementtypeid]
	(
		@ElementTypeID	[nvarchar](15)
	)
AS
	BEGIN
		DELETE 	[ElementType]
		WHERE 	[ElementType].[ElementTypeID] = @ElementTypeID
		RETURN 	@@ROWCOUNT;
	END
GO

PRINT '*** creating sp_select_elements ***'
GO
CREATE PROCEDURE [dbo].[sp_select_elements]
AS
	BEGIN
		SELECT 	[ElementType].[ElementTypeID],[ElementType].[Description]
		FROM	[ElementType];
	END
GO

PRINT '*** creating sp_select_artist_by_artistid ***'
GO
CREATE PROCEDURE [dbo].[sp_select_artist_by_artistid]
	(
		@ArtistID		[int]
	)
AS
	BEGIN
		SELECT 	[Artist].[ArtistID],[Artist].[GivenName],[Artist].[Surname]
		FROM	[Artist]
		WHERE	[Artist].[ArtistID] = @ArtistID;
	END
GO

PRINT '*** creating sp_select_artist_by_given_name ***'
GO
CREATE PROCEDURE [dbo].[sp_select_artist_by_name]
	(
		@GivenName		[nvarchar](50),
		@Surname		[nvarchar](100)
	)
AS
	BEGIN
		SELECT 	[Artist].[ArtistID],[Artist].[GivenName],[Artist].[Surname]
		FROM	[Artist]
		WHERE	[Artist].[GivenName] = @GivenName
		AND		[Artist].[Surname] = @Surname;
	END
GO

PRINT '*** creating sp_insert_artist_into_artist ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_artist]
	(
		@GivenName		[nvarchar](50),
		@Surname		[nvarchar](100)
	)
AS
	BEGIN
		INSERT INTO [dbo].[Artist]
			([GivenName],[Surname])
		VALUES
			(@GivenName,@Surname)
		RETURN SCOPE_IDENTITY();
	END
GO

PRINT '*** creating sp_update_artist_by_artistid ***'
GO
CREATE PROCEDURE [dbo].[sp_update_artist_by_artistid]
	(
		@ArtistID		[int],
		@GivenName		[nvarchar](50),
		@Surname		[nvarchar](100)
	)
AS
	BEGIN
		UPDATE 	[Artist]
		SET		[Artist].[GivenName] = @GivenName,
				[Artist].[Surname] = @Surname
		WHERE	[Artist].[ArtistID] = @ArtistID
		RETURN	@@ROWCOUNT;
	END
GO

PRINT '*** creating sp_delete_artist_by_artistid ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_artist_by_artistid]
	(
		@ArtistID		[int]
	)
AS
	BEGIN
		DELETE 	[Artist]
		WHERE 	[Artist].[ArtistID] = @ArtistID
		RETURN 	@@ROWCOUNT;
	END
GO

PRINT '*** creating sp_select_artists ***'
GO
CREATE PROCEDURE [dbo].[sp_select_artists]
AS
	BEGIN
		SELECT 	[Artist].[ArtistID],[Artist].[GivenName],[Artist].[Surname]
		FROM	[Artist];
	END
GO

PRINT '*** creating sp_select_booster_by_boosterid ***'
GO
CREATE PROCEDURE [dbo].[sp_select_booster_by_boosterid]
	(
		@BoosterID		[nvarchar](50)
	)
AS
	BEGIN
		SELECT 	[Booster].[BoosterID],[Booster].[Series],
				[Booster].[ReleaseDate],[Booster].[Abbreviation]
		FROM	[Booster]
		WHERE	[Booster].[BoosterID] = @BoosterID;
	END
GO

PRINT '*** creating sp_insert_booster ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_booster]
	(
		@BoosterID		[nvarchar](50),
		@Series			[nvarchar](50),
		@ReleaseDate	[date],
		@Abbreviation	[nvarchar](4)
	)	
AS
	BEGIN
		INSERT INTO [dbo].[Booster]
			([BoosterID],[Series],[ReleaseDate],[Abbreviation])
		VALUES
			(@BoosterID,@Series,@ReleaseDate,@Abbreviation)
		RETURN @@ROWCOUNT;
	END
GO

PRINT '*** creating sp_update_booster_by_boosterid ***'
GO
CREATE PROCEDURE [dbo].[sp_update_booster_by_boosterid]
	(
		@BoosterID		[nvarchar](50),
		@Series			[nvarchar](50),
		@ReleaseDate	[date],
		@Abbreviation	[nvarchar](4)
	)
AS
	BEGIN
		UPDATE 	[Booster]
		SET		[Booster].[Series] = @Series,
				[Booster].[ReleaseDate] = @ReleaseDate,
				[Booster].[Abbreviation] = @Abbreviation
		WHERE	[Booster].[BoosterID] = @BoosterID
		RETURN	@@ROWCOUNT;
	END
GO

PRINT '*** creating sp_delete_booster_by_boosterid ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_booster_by_boosterid]
	(
		@BoosterID		[nvarchar](50)
	)
AS
	BEGIN
		DELETE 	[Booster]
		WHERE 	[Booster].[BoosterID] = @BoosterID
		RETURN 	@@ROWCOUNT;
	END
GO

PRINT '*** creating sp_select_boosters ***'
GO
CREATE PROCEDURE [dbo].[sp_select_boosters]
AS
	BEGIN
		SELECT 	[Booster].[BoosterID],[Booster].[Series],
				[Booster].[ReleaseDate],[Booster].[Abbreviation]
		FROM	[Booster];
	END
GO


/*
Work on booster sp

When going to the create records page from the edit button
make the other tabs not clickable.

*/

