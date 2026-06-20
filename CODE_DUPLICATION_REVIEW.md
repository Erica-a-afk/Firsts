# Code Duplication Review

Scope: `Assets/01.Scripts`

Excluded from review: external/vendor assets, DOTween, SPUM, DarkPixelRPGUI, and generated Codex Unity MCP bridge code.

## Summary

The project has several repeated patterns, but not all should be merged. The highest-value cleanup is to standardize duplicated health/damage proxy code, UI HUD updates, wall-state FSM logic, and runtime singleton persistence.

## Priority 1: Runtime Bugs / Structural Drift

### SceneFlowManager persistence

File: `Assets/01.Scripts/5_Managers/SceneFlowManager.cs`

`SceneFlowManager` was calling `DontDestroyOnLoad(gameObject)` while the component is under `GameManager/SceneFlow`. Unity requires `DontDestroyOnLoad` to receive a root GameObject.

Applied fix:

```csharp
DontDestroyOnLoad(transform.root.gameObject);
```

### Boss intro public entry point

Files:

- `Assets/01.Scripts/4_Interactables/WarpGate.cs`
- `Assets/01.Scripts/3_Boss/BossIntroSequenceController.cs`

`WarpGate` called `BossIntroSequenceController.PlayIntroFromBossPortal()`, but the method did not exist. The smallest safe fix was to expose a public wrapper around the existing intro flow.

## Priority 2: Real Duplicate Functionality

### IDamageable proxy duplication

Files:

- `Assets/01.Scripts/1_Player/PlayerDamageReceiver.cs`
- `Assets/01.Scripts/2_Enemies/EnemyReferences.cs`
- `Assets/01.Scripts/3_Boss/BossReferences.cs`

Repeated pattern:

- `CurrentHealth` getter/setter delegates to a stats component
- `MaxHealth` getter/setter delegates to a stats component
- `IsDead` delegates to stats
- `ReceiveDamage`, `TakeDamage`, `Die` delegate to stats

Recommended cleanup:

Create a small reusable helper or base class for damage forwarding. Do not merge `PlayerStatsModule`, `EnemyStats`, `BossStats`, and `TutorialTrainingDummy` into one health class yet, because their behavior differs substantially.

### UI HUD update duplication

Files:

- `Assets/01.Scripts/1_Player/PlayerUIBinder.cs`
- `Assets/01.Scripts/5_Managers/GameUIManager.cs`

Repeated pattern:

- gauge fill ratio calculation
- potion icon alpha update
- gold text update
- interaction prompt activation

Design issue:

`PlayerUIBinder` uses direct `PlayerStatsModule` references, while `GameUIManager` uses event channels. Pick one UI ownership model.

Recommended direction:

Use event channels for scene/global UI and remove duplicate direct HUD update paths unless a local player-only binder is intentionally needed.

### Player wall state duplication

Files:

- `Assets/01.Scripts/1_Player/FSM/States/Air/PlayerWallHoldState.cs`
- `Assets/01.Scripts/1_Player/FSM/States/Air/PlayerWallSlideStopState.cs`
- `Assets/01.Scripts/1_Player/FSM/States/Air/PlayerWallTransitionState.cs`

Repeated pattern:

- freeze rigidbody
- reset gravity on exit
- fall when wall contact is lost
- wall jump check
- transition to wall slide based on input/drop

Recommended cleanup:

Extract a `PlayerWallFreezeStateBase` or shared helper in FSM services.

### AnimationSetSO validation duplication

Files:

- `Assets/01.Scripts/0_Core/ScriptableObjects/Animation/PlayerAnimationSetSO.cs`
- `Assets/01.Scripts/0_Core/ScriptableObjects/Animation/EnemyAnimationSetSO.cs`
- `Assets/01.Scripts/0_Core/ScriptableObjects/Animation/BossAnimationSetSO.cs`

Repeated pattern:

- `SafeName(string value, string fallback)`
- long `OnValidate` fallback assignment lists

Recommended cleanup:

Move `SafeName` into a shared static utility. Avoid forcing a common base ScriptableObject unless the assets begin sharing more than validation.

## Priority 3: Repeated Patterns To Standardize Later

### Singleton / duplicate prevention

Files include:

- `HitStopController`
- `SceneBgmPlayer`
- `UIButtonClickSoundPlayer`
- `SceneFlowManager`
- `GameUIManager`
- `SaveManager`

Repeated pattern:

- static instance
- duplicate destroy
- optional `DontDestroyOnLoad`

Recommended cleanup:

Introduce a small `RuntimeSingleton<T>` only if the team accepts generic MonoBehaviour bases. Otherwise create a `PersistentRootUtility` and keep explicit singleton code.

### FindObject fallback lookup

Files include:

- `PlayerRoomSpawner`
- `RoomCameraConfinerSwitcher`
- `WarpGate`
- `RoomManager`
- `SceneFlowManager`
- `BossIntroSequenceController`

Recommended cleanup:

Prefer serialized references and scene bootstrap wiring. Use `FindAnyObjectByType` as a last-resort fallback. Replace obsolete `FindFirstObjectByType` usages when touched.

### Combat feedback raising

Files include:

- `PlayerStatsModule.Events.cs`
- `EnemyStats.cs`
- `BossStats.cs`
- `AgentAttackBase.cs`

Recommended cleanup:

Create a small `CombatFeedbackEmitter` helper that owns null checks and `CombatFeedbackData` construction.

## Do Not Merge Yet

### Health implementations

Do not immediately merge these into a single base:

- `PlayerStatsModule`
- `EnemyStats`
- `BossStats`
- `TutorialTrainingDummy`

Reason:

They share health/event shape, but their rules are different: player guard/parry/shield, boss phase/invincibility/destroy, enemy reward/death channel, tutorial dummy immortality.

### Player attack states

`PlayerComboAttackState` and `PlayerTimedAttackState` share some hit timing and gizmo code, but their transition logic differs. Extract only small helpers first, not a large abstract attack state.

## Recommended Refactor Order

1. Fix runtime warnings and compile errors.
2. Add a damage forwarding helper for `IDamageable` proxy classes.
3. Standardize UI HUD ownership around event channels or direct binder, not both.
4. Extract wall-freeze FSM shared logic.
5. Move animation `SafeName` validation into a utility.
6. Replace obsolete object-finding APIs gradually when touching each file.

