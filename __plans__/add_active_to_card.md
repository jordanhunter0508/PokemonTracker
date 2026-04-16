# Implement Active Field for PokemonCard

The `PokemonCard` table in the database contains an `Active` (bit) field, but this field is currently unused in the application's domain model, data access layers, and UI. Enabling this allows for soft-deleting and managing the visible state of the cards without permanently deleting them from the database.

## User Review Required

> [!WARNING]
> Since we are treating `Active` as a soft-delete mechanism, `Active` defaults to 0 in the `create_db.sql` database schema for new cards. If this is undesirable (i.e. if cards should activate upon creation), we should either update the DB default constraint or explicitly insert them as `Active = 1`. For this plan, I assume we will modify `sp_insert_card` to set `Active` explicitly to `1` by default upon creation, or update the schema default. Please confirm!

## Proposed Changes

### DataDomain

#### [MODIFY] Card.cs
- Add `public bool Active { get; set; }` to the `Card` entity class to map to the `Active` database column.

---

### DataAccess

#### [MODIFY] ICardAccessor.cs
- Add `int ReactivateCard(int cardID)` to support restoring deactivated cards.
- Add `int DeactivateCard(int cardID)` to support soft deletion.

#### [MODIFY] CardAccessor.cs
- Implement `ReactivateCard(int cardID)` using a new `sp_reactivate_card` stored procedure.
- Implement `DeactivateCard(int cardID)` using a new `sp_deactivate_card` stored procedure.
- Update `SelectAllCards` reading logic to map the new `Active` column if we include it in the SP.
- Update `SelectCardsPaginated` reading logic to map `Active` (if returning active cards).
- Update `UpdateCard` to persist the `Active` property.

---

### LogicLayer

#### [MODIFY] ICardManager.cs
- Add `bool ReactivateCard(int cardID)` method.
- Add `bool DeactivateCard(int cardID)` method.

#### [MODIFY] CardManager.cs
- Implement `ReactivateCard(int cardID)` which delegates to the `ICardAccessor`.
- Implement `DeactivateCard(int cardID)` which delegates to the `ICardAccessor`.

---

### Database (tcg_db)

#### [MODIFY] create_stored_procedures.sql
- **New Proc (`sp_reactivate_card`)**: Sets `Active = 1` for a `PokemonCardID`.
- **New Proc (`sp_deactivate_card`)**: Sets `Active = 0` for a `PokemonCardID`.
- Update `sp_select_all_cards` to return `[Active]`.
- Update `sp_select_cards_paginated` to return `[Active]`.
- Update `sp_select_cards_by_card_name` to return `[Active]`.
- Update `sp_update_card` to accept an `Active` bit and save it if needed.

## Open Questions

> [!IMPORTANT]
> 1. Should we also update `sp_delete_card` to become a soft delete (where it simply calls deactivate instead of issuing a true DELETE statement), or should deleting stay a hard deletion from the DB?
> 2. Does the presentation layer (WPF/Web) need specific new UI controls added to support viewing/reactivating inactive records right now? 

## Verification Plan

### Automated Tests
- Review existing `CardManagerTest` in `LogicLayerTest`.
- Add test coverage for `ReactivateCard` and `DeactivateCard` in unit test fakes.

### Manual Verification
- Rebuild database via `db_script.bat`.
- Run the WPF client, manually interact with a card and perform Reactivate/Deactivate actions via logic.
