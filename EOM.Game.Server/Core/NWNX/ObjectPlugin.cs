using System.Numerics;
using EOM.Game.Server.Core.NWNX.Enum;

namespace EOM.Game.Server.Core.NWNX
{

    public class ObjectPlugin
    {
        /// @addtogroup object Object
        /// Functions exposing additional object properties.
        /// @{
        /// @file nwnx_object.nss
        public const string NWNX_Object = "NWNX_Object";

        ///&lt; @private
        /// @anchor object_localvar_types
        /// @name Local Variable Types
        /// @{
        public const int NWNX_OBJECT_LOCALVAR_TYPE_UNKNOWN = 0;
        public const int NWNX_OBJECT_LOCALVAR_TYPE_INT = 1;
        public const int NWNX_OBJECT_LOCALVAR_TYPE_FLOAT = 2;
        public const int NWNX_OBJECT_LOCALVAR_TYPE_STRING = 3;
        public const int NWNX_OBJECT_LOCALVAR_TYPE_OBJECT = 4;
        public const int NWNX_OBJECT_LOCALVAR_TYPE_LOCATION = 5;
        public const int NWNX_OBJECT_LOCALVAR_TYPE_JSON = 6;

        // @}
        /// @anchor object_internal_types
        /// @name Internal Object Types
        /// @{
        public const int NWNX_OBJECT_TYPE_INTERNAL_INVALID = -1;
        public const int NWNX_OBJECT_TYPE_INTERNAL_GUI = 1;
        public const int NWNX_OBJECT_TYPE_INTERNAL_TILE = 2;
        public const int NWNX_OBJECT_TYPE_INTERNAL_MODULE = 3;
        public const int NWNX_OBJECT_TYPE_INTERNAL_AREA = 4;
        public const int NWNX_OBJECT_TYPE_INTERNAL_CREATURE = 5;
        public const int NWNX_OBJECT_TYPE_INTERNAL_ITEM = 6;
        public const int NWNX_OBJECT_TYPE_INTERNAL_TRIGGER = 7;
        public const int NWNX_OBJECT_TYPE_INTERNAL_PROJECTILE = 8;
        public const int NWNX_OBJECT_TYPE_INTERNAL_PLACEABLE = 9;
        public const int NWNX_OBJECT_TYPE_INTERNAL_DOOR = 10;
        public const int NWNX_OBJECT_TYPE_INTERNAL_AREAOFEFFECT = 11;
        public const int NWNX_OBJECT_TYPE_INTERNAL_WAYPOINT = 12;
        public const int NWNX_OBJECT_TYPE_INTERNAL_ENCOUNTER = 13;
        public const int NWNX_OBJECT_TYPE_INTERNAL_STORE = 14;
        public const int NWNX_OBJECT_TYPE_INTERNAL_PORTAL = 15;
        public const int NWNX_OBJECT_TYPE_INTERNAL_SOUND = 16;

        // @}
        /// @anchor projectile_types
        /// @name Projectile VFX Types
        /// @{
        public const int NWNX_OBJECT_SPELL_PROJECTILE_TYPE_DEFAULT = 6;
        public const int NWNX_OBJECT_SPELL_PROJECTILE_TYPE_USE_PATH = 7;

        // @}
        /// A local variable structure.
        /// Gets the count of all local variables.
        /// <param name="obj">The object.</param>
        /// <returns>The count.</returns>
        public static int GetLocalVariableCount(uint obj)
        {
            NWNXPushObject(obj);
            NWNXCall(NWNX_Object, "GetLocalVariableCount");
            return NWNXPopInt();
        }

        /// Gets the local variable at the provided index of the provided object.
        /// <param name="obj">The object.</param>
        /// <param name="index">The index.</param>
        /// @note Index bounds: 0 &gt;= index &lt; NWNX_Object_GetLocalVariableCount().
        /// @note As of build 8193.14 local variables no longer have strict ordering.
        ///       this means that any change to the variables can result in drastically
        ///       different order when iterating.
        /// @note As of build 8193.14, this function takes O(n) time, where n is the number
        ///       of locals on the object. Individual variable access with GetLocalXxx()
        ///       is now O(1) though.
        /// @note As of build 8193.14, this function will not return a variable if the value is
        ///       the default (0/0.0/&quot;&quot;/OBJECT_INVALID/JsonNull()) for the type. They are considered not set.
        /// @note Will return type UNKNOWN for cassowary variables.
        /// <returns>An NWNX_Object_LocalVariable struct.</returns>
        public static LocalVariable GetLocalVariable(uint obj, int index)
        {
            NWNXPushInt(index);
            NWNXPushObject(obj);
            NWNXCall(NWNX_Object, "GetLocalVariable");
            LocalVariable var = default;
            var.key = NWNXPopString();
            var.type = NWNXPopInt();
            return var;
        }

        /// Set oObject&apos;s position.
        /// <param name="oObject">The object.</param>
        /// <param name="vPosition">A vector position.</param>
        /// <param name="bUpdateSubareas">If TRUE and oObject is a creature, any triggers/traps at vPosition will fire their events.</param>
        public static void SetPosition(uint oObject, System.Numerics.Vector3 vPosition, int bUpdateSubareas = 1)
        {
            NWNXPushInt(bUpdateSubareas);
            NWNXPushVector(vPosition);
            NWNXPushObject(oObject);
            NWNXCall(NWNX_Object, "SetPosition");
        }

        /// Get an object&apos;s hit points.
        /// @note Unlike the native GetCurrentHitpoints function, this excludes temporary hitpoints.
        /// <param name="obj">The object.</param>
        /// <returns>The hit points.</returns>
        public static int GetCurrentHitPoints(uint creature)
        {
            NWNXPushObject(creature);
            NWNXCall(NWNX_Object, "GetCurrentHitPoints");
            return NWNXPopInt();
        }

        /// Set an object&apos;s hit points.
        /// <param name="obj">The object.</param>
        /// <param name="hp">The hit points.</param>
        public static void SetCurrentHitPoints(uint creature, int hp)
        {
            NWNXPushInt(hp);
            NWNXPushObject(creature);
            NWNXCall(NWNX_Object, "SetCurrentHitPoints");
        }

        /// Adjust an object&apos;s maximum hit points
        /// @note Will not work on PCs.
        /// <param name="obj">The object.</param>
        /// <param name="hp">The maximum hit points.</param>
        public static void SetMaxHitPoints(uint creature, int hp)
        {
            NWNXPushInt(hp);
            NWNXPushObject(creature);
            NWNXCall(NWNX_Object, "SetMaxHitPoints");
        }

        /// Serialize a full object to a base64 string
        /// <param name="obj">The object.</param>
        /// <returns>A base64 string representation of the object.</returns>
        /// @note includes locals, inventory, etc
        public static string Serialize(uint obj)
        {
            NWNXPushObject(obj);
            NWNXCall(NWNX_Object, "Serialize");
            return NWNXPopString();
        }

        /// Deserialize the object.
        /// @note The object will be created outside of the world and needs to be manually positioned at a location/inventory.
        /// <param name="serialized">The base64 string.</param>
        /// <returns>The object.</returns>
        public static uint Deserialize(string serialized)
        {
            NWNXPushString(serialized);
            NWNXCall(NWNX_Object, "Deserialize");
            return NWNXPopObject();
        }

        /// Gets the dialog resref.
        /// <param name="obj">The object.</param>
        /// <returns>The name of the dialog resref.</returns>
        public static string GetDialogResref(uint obj)
        {
            NWNXPushObject(obj);
            NWNXCall(NWNX_Object, "GetDialogResref");
            return NWNXPopString();
        }

        /// Sets the dialog resref.
        /// <param name="obj">The object.</param>
        /// <param name="dialog">The name of the dialog resref.</param>
        public static void SetDialogResref(uint obj, string dialog)
        {
            NWNXPushString(dialog);
            NWNXPushObject(obj);
            NWNXCall(NWNX_Object, "SetDialogResref");
        }

        /// Set oPlaceable&apos;s appearance.
        /// @note Will not update for PCs until they re-enter the area.
        /// <param name="oPlaceable">The placeable.</param>
        /// <param name="nAppearance">The appearance id.</param>
        public static void SetAppearance(uint oPlaceable, int nAppearance)
        {
            NWNXPushInt(nAppearance);
            NWNXPushObject(oPlaceable);
            NWNXCall(NWNX_Object, "SetAppearance");
        }

        /// Get oPlaceable&apos;s appearance.
        /// <param name="oPlaceable">The placeable.</param>
        /// <returns>The appearance id.</returns>
        public static int GetAppearance(uint oPlaceable)
        {
            NWNXPushObject(oPlaceable);
            NWNXCall(NWNX_Object, "GetAppearance");
            return NWNXPopInt();
        }

        /// Determine if an object has a visual effect.
        /// <param name="obj">The object.</param>
        /// <param name="nVFX">The visual effect id.</param>
        /// <returns>TRUE if the object has the visual effect applied to it</returns>
        public static int GetHasVisualEffect(uint obj, int nVFX)
        {
            NWNXPushInt(nVFX);
            NWNXPushObject(obj);
            NWNXCall(NWNX_Object, "GetHasVisualEffect");
            return NWNXPopInt();
        }

        /// Get an object&apos;s damage immunity.
        /// <param name="obj">The object.</param>
        /// <param name="damageType">The damage type to check for immunity. Use DAMAGE_TYPE_* constants.</param>
        /// <returns>Damage immunity as a percentage.</returns>
        public static int GetDamageImmunity(uint obj, int damageType)
        {
            NWNXPushInt(damageType);
            NWNXPushObject(obj);
            NWNXCall(NWNX_Object, "GetDamageImmunity");
            return NWNXPopInt();
        }

        /// Add or move an object.
        /// <param name="obj">The object.</param>
        /// <param name="area">The area.</param>
        /// <param name="pos">The position.</param>
        public static void AddToArea(uint obj, uint area, System.Numerics.Vector3 pos)
        {
            NWNXPushVector(pos);
            NWNXPushObject(area);
            NWNXPushObject(obj);
            NWNXCall(NWNX_Object, "AddToArea");
        }

        /// Get placeable&apos;s static setting
        /// <param name="obj">The object.</param>
        /// <returns>TRUE if placeable is static.</returns>
        public static int GetPlaceableIsStatic(uint obj)
        {
            NWNXPushObject(obj);
            NWNXCall(NWNX_Object, "GetPlaceableIsStatic");
            return NWNXPopInt();
        }

        /// Set placeable as static or not.
        /// @note Will not update for PCs until they re-enter the area.
        /// <param name="obj">The object.</param>
        /// <param name="isStatic">TRUE/FALSE</param>
        public static void SetPlaceableIsStatic(uint obj, int isStatic)
        {
            NWNXPushInt(isStatic);
            NWNXPushObject(obj);
            NWNXCall(NWNX_Object, "SetPlaceableIsStatic");
        }

        /// Gets if a door/placeable auto-removes the key after use.
        /// <param name="obj">The object.</param>
        /// <returns>TRUE/FALSE or -1 on error.</returns>
        public static int GetAutoRemoveKey(uint obj)
        {
            NWNXPushObject(obj);
            NWNXCall(NWNX_Object, "GetAutoRemoveKey");
            return NWNXPopInt();
        }

        /// Sets if a door/placeable auto-removes the key after use.
        /// <param name="obj">The object.</param>
        /// <param name="bRemoveKey">TRUE/FALSE</param>
        public static void SetAutoRemoveKey(uint obj, int bRemoveKey)
        {
            NWNXPushInt(bRemoveKey);
            NWNXPushObject(obj);
            NWNXCall(NWNX_Object, "SetAutoRemoveKey");
        }

        /// Get the geometry of a trigger
        /// <param name="oTrigger">The trigger object.</param>
        /// <returns>A string of vertex positions.</returns>
        public static string GetTriggerGeometry(uint oTrigger)
        {
            NWNXPushObject(oTrigger);
            NWNXCall(NWNX_Object, "GetTriggerGeometry");
            return NWNXPopString();
        }

        /// Set the geometry of a trigger with a list of vertex positions
        /// <param name="oTrigger">The trigger object.</param>
        /// <param name="sGeometry">Needs to be in the following format -&gt; {x.x, y.y, z.z} or {x.x, y.y}</param>
        /// Example Geometry: &quot;{1.0, 1.0, 0.0}{4.0, 1.0, 0.0}{4.0, 4.0, 0.0}{1.0, 4.0, 0.0}&quot;
        ///
        /// @remark The Z position is optional and will be calculated dynamically based
        /// on terrain height if it&apos;s not provided.
        ///
        /// @remark The minimum number of vertices is 3.
        public static void SetTriggerGeometry(uint oTrigger, string sGeometry)
        {
            NWNXPushString(sGeometry);
            NWNXPushObject(oTrigger);
            NWNXCall(NWNX_Object, "SetTriggerGeometry");
        }

        /// Export an object to the UserDirectory/nwnx folder.
        /// <param name="sFileName">The filename without extension, 16 or less characters.</param>
        /// <param name="oObject">The object to export. Valid object types: Creature, Item, Placeable, Waypoint, Door, Store, Trigger</param>
        /// <param name="sAlias">The alias of the resource directory to add the .git file to. Default: UserDirectory/nwnx</param>
        public static void Export(uint oObject, string sFileName, string sAlias = "NWNX")
        {
            NWNXPushString(sAlias);
            NWNXPushString(sFileName);
            NWNXPushObject(oObject);
            NWNXCall(NWNX_Object, "Export");
        }

        /// Get oObject&apos;s integer variable sVarName.
        /// <param name="oObject">The object to get the variable from.</param>
        /// <param name="sVarName">The variable name.</param>
        /// <returns>The value or 0 on error.</returns>
        public static int GetInt(uint oObject, string sVarName)
        {
            NWNXPushString(sVarName);
            NWNXPushObject(oObject);
            NWNXCall(NWNX_Object, "GetInt");
            return NWNXPopInt();
        }

        /// Set oObject&apos;s integer variable sVarName to nValue.
        /// <param name="oObject">The object to set the variable on.</param>
        /// <param name="sVarName">The variable name.</param>
        /// <param name="nValue">The integer value to to set</param>
        /// <param name="bPersist">When TRUE, the value is persisted to GFF, this means that it&apos;ll be saved in the .bic file of a player&apos;s character or when an object is serialized.</param>
        public static void SetInt(uint oObject, string sVarName, int nValue, int bPersist)
        {
            NWNXPushInt(bPersist);
            NWNXPushInt(nValue);
            NWNXPushString(sVarName);
            NWNXPushObject(oObject);
            NWNXCall(NWNX_Object, "SetInt");
        }

        /// Delete oObject&apos;s integer variable sVarName.
        /// <param name="oObject">The object to delete the variable from.</param>
        /// <param name="sVarName">The variable name.</param>
        public static void DeleteInt(uint oObject, string sVarName)
        {
            NWNXPushString(sVarName);
            NWNXPushObject(oObject);
            NWNXCall(NWNX_Object, "DeleteInt");
        }

        /// Get oObject&apos;s string variable sVarName.
        /// <param name="oObject">The object to get the variable from.</param>
        /// <param name="sVarName">The variable name.</param>
        /// <returns>The value or &quot;&quot; on error.</returns>
        public static string GetString(uint oObject, string sVarName)
        {
            NWNXPushString(sVarName);
            NWNXPushObject(oObject);
            NWNXCall(NWNX_Object, "GetString");
            return NWNXPopString();
        }

        /// Set oObject&apos;s string variable sVarName to sValue.
        /// <param name="oObject">The object to set the variable on.</param>
        /// <param name="sVarName">The variable name.</param>
        /// <param name="sValue">The string value to to set</param>
        /// <param name="bPersist">When TRUE, the value is persisted to GFF, this means that it&apos;ll be saved in the .bic file of a player&apos;s character or when an object is serialized.</param>
        public static void SetString(uint oObject, string sVarName, string sValue, int bPersist)
        {
            NWNXPushInt(bPersist);
            NWNXPushString(sValue);
            NWNXPushString(sVarName);
            NWNXPushObject(oObject);
            NWNXCall(NWNX_Object, "SetString");
        }

        /// Delete oObject&apos;s string variable sVarName.
        /// <param name="oObject">The object to delete the variable from.</param>
        /// <param name="sVarName">The variable name.</param>
        public static void DeleteString(uint oObject, string sVarName)
        {
            NWNXPushString(sVarName);
            NWNXPushObject(oObject);
            NWNXCall(NWNX_Object, "DeleteString");
        }

        /// Get oObject&apos;s float variable sVarName.
        /// <param name="oObject">The object to get the variable from.</param>
        /// <param name="sVarName">The variable name.</param>
        /// <returns>The value or 0.0f on error.</returns>
        public static float GetFloat(uint oObject, string sVarName)
        {
            NWNXPushString(sVarName);
            NWNXPushObject(oObject);
            NWNXCall(NWNX_Object, "GetFloat");
            return NWNXPopFloat();
        }

        /// Set oObject&apos;s float variable sVarName to fValue.
        /// <param name="oObject">The object to set the variable on.</param>
        /// <param name="sVarName">The variable name.</param>
        /// <param name="fValue">The float value to to set</param>
        /// <param name="bPersist">When TRUE, the value is persisted to GFF, this means that it&apos;ll be saved in the .bic file of a player&apos;s character or when an object is serialized.</param>
        public static void SetFloat(uint oObject, string sVarName, float fValue, int bPersist)
        {
            NWNXPushInt(bPersist);
            NWNXPushFloat(fValue);
            NWNXPushString(sVarName);
            NWNXPushObject(oObject);
            NWNXCall(NWNX_Object, "SetFloat");
        }

        /// Delete oObject&apos;s persistent float variable sVarName.
        /// <param name="oObject">The object to delete the variable from.</param>
        /// <param name="sVarName">The variable name.</param>
        public static void DeleteFloat(uint oObject, string sVarName)
        {
            NWNXPushString(sVarName);
            NWNXPushObject(oObject);
            NWNXCall(NWNX_Object, "DeleteFloat");
        }

        /// Delete any variables that match sRegex
        /// @note It will only remove variables set by NWNX_Object_Set{Int|String|Float}()
        /// <param name="oObject">The object to delete the variables from.</param>
        /// <param name="sRegex">The regular expression, for example .*Test.* removes every variable that has Test in it.</param>
        public static void DeleteVarRegex(uint oObject, string sRegex)
        {
            NWNXPushString(sRegex);
            NWNXPushObject(oObject);
            NWNXCall(NWNX_Object, "DeleteVarRegex");
        }

        /// Get if vPosition is inside oTrigger&apos;s geometry.
        /// @note The Z value of vPosition is ignored.
        /// <param name="oTrigger">The trigger.</param>
        /// <param name="vPosition">The position.</param>
        /// <returns>TRUE if vPosition is inside oTrigger&apos;s geometry.</returns>
        public static int GetPositionIsInTrigger(uint oTrigger, System.Numerics.Vector3 vPosition)
        {
            NWNXPushVector(vPosition);
            NWNXPushObject(oTrigger);
            NWNXCall(NWNX_Object, "GetPositionIsInTrigger");
            return NWNXPopInt();
        }

        /// Gets the given object&apos;s internal type (NWNX_OBJECT_TYPE_INTERNAL_*)
        /// <param name="oObject">The object.</param>
        /// <returns>The object&apos;s type (NWNX_OBJECT_TYPE_INTERNAL_*)</returns>
        public static int GetInternalObjectType(uint oObject)
        {
            NWNXPushObject(oObject);
            NWNXCall(NWNX_Object, "GetInternalObjectType");
            return NWNXPopInt();
        }

        /// Have oObject acquire oItem.
        /// @note Useful to give deserialized items to an object, may not work if oItem is already possessed by an object.
        /// <param name="oObject">The object receiving oItem, must be a Creature, Placeable, Store or Item</param>
        /// <param name="oItem">The item.</param>
        /// <returns>TRUE on success.</returns>
        public static int AcquireItem(uint oObject, uint oItem)
        {
            NWNXPushObject(oItem);
            NWNXPushObject(oObject);
            NWNXCall(NWNX_Object, "AcquireItem");
            return NWNXPopInt();
        }

        /// Clear all spell effects oObject has applied to others.
        /// <param name="oObject">The object that applied the spell effects.</param>
        public static void ClearSpellEffectsOnOthers(uint oObject)
        {
            NWNXPushObject(oObject);
            NWNXCall(NWNX_Object, "ClearSpellEffectsOnOthers");
        }

        /// Peek at the UUID of oObject without assigning one if it does not have one
        /// <param name="oObject">The object</param>
        /// <returns>The UUID or &quot;&quot; when the object does not have or cannot have an UUID</returns>
        public static string PeekUUID(uint oObject)
        {
            NWNXPushObject(oObject);
            NWNXCall(NWNX_Object, "PeekUUID");
            return NWNXPopString();
        }

        /// Get if oDoor has a visible model.
        /// <param name="oDoor">The door</param>
        /// <returns>TRUE if oDoor has a visible model</returns>
        public static int GetDoorHasVisibleModel(uint oDoor)
        {
            NWNXPushObject(oDoor);
            NWNXCall(NWNX_Object, "GetDoorHasVisibleModel");
            return NWNXPopInt();
        }

        /// Get if oObject is destroyable.
        /// <param name="oObject">The object</param>
        /// <returns>TRUE if oObject is destroyable.</returns>
        public static int GetIsDestroyable(uint oObject)
        {
            NWNXPushObject(oObject);
            NWNXCall(NWNX_Object, "GetIsDestroyable");
            return NWNXPopInt();
        }

        /// Checks for specific spell immunity. Should only be called in spellscripts
        /// <param name="oDefender">The object defending against the spell.</param>
        /// <param name="oCaster">The object casting the spell.</param>
        /// <param name="nSpellId">The casted spell id. Default value is -1, which corrresponds to the normal game behaviour.</param>
        /// <returns>-1 if defender has no immunity, 2 if the defender is immune</returns>
        public static int DoSpellImmunity(uint oDefender, uint oCaster, int nSpellId = -1)
        {
            NWNXPushInt(nSpellId);
            NWNXPushObject(oCaster);
            NWNXPushObject(oDefender);
            NWNXCall(NWNX_Object, "DoSpellImmunity");
            return NWNXPopInt();
        }

        /// Checks for spell school/level immunities and mantles. Should only be called in spellscripts
        /// <param name="oDefender">The object defending against the spell.</param>
        /// <param name="oCaster">The object casting the spell.</param>
        /// <param name="nSpellId">The casted spell id. Default value is -1, which corrresponds to the normal game behaviour.</param>
        /// <param name="nSpellLevel">The level of the casted spell. Default value is -1, which corrresponds to the normal game behaviour.</param>
        /// <param name="nSpellSchool">The school of the casted spell (SPELL_SCHOOL_* constant). Default value is -1, which corrresponds to the normal game behaviour.</param>
        /// <returns>-1 defender no immunity. 2 if immune. 3 if immune, but the immunity has a limit (example: mantles)</returns>
        public static int DoSpellLevelAbsorption(uint oDefender, uint oCaster, int nSpellId = -1, int nSpellLevel = -1, int nSpellSchool = -1)
        {
            NWNXPushInt(nSpellSchool);
            NWNXPushInt(nSpellLevel);
            NWNXPushInt(nSpellId);
            NWNXPushObject(oCaster);
            NWNXPushObject(oDefender);
            NWNXCall(NWNX_Object, "DoSpellLevelAbsorption");
            return NWNXPopInt();
        }

        /// Sets if a placeable has an inventory.
        /// <param name="obj">The placeable.</param>
        /// <param name="bHasInventory">TRUE/FALSE</param>
        /// @note Only works on placeables.
        public static void SetHasInventory(uint obj, int bHasInventory)
        {
            NWNXPushInt(bHasInventory);
            NWNXPushObject(obj);
            NWNXCall(NWNX_Object, "SetHasInventory");
        }

        /// Get the current animation of oObject
        /// @note The returned value will be an engine animation constant, not a NWScript ANIMATION_ constant.
        ///       See: https://github.com/nwnxee/unified/blob/master/NWNXLib/API/Constants/Animation.hpp
        /// <param name="oObject">The object</param>
        /// <returns>-1 on error or the engine animation constant</returns>
        public static int GetCurrentAnimation(uint oObject)
        {
            NWNXPushObject(oObject);
            NWNXCall(NWNX_Object, "GetCurrentAnimation");
            return NWNXPopInt();
        }

        /// Gets the AI level of an object.
        /// <param name="oObject">The object.</param>
        /// <returns>The AI level (AI_LEVEL_* -1 to 4).</returns>
        public static int GetAILevel(uint oObject)
        {
            NWNXPushObject(oObject);
            NWNXCall(NWNX_Object, "GetAILevel");
            return NWNXPopInt();
        }

        /// Sets the AI level of an object.
        /// <param name="oObject">The object.</param>
        /// <param name="nLevel">The level to set (AI_LEVEL_* -1 to 4).</param>
        public static void SetAILevel(uint oObject, int nLevel)
        {
            NWNXPushInt(nLevel);
            NWNXPushObject(oObject);
            NWNXCall(NWNX_Object, "SetAILevel");
        }

        /// Retrieves the Map Note (AKA Map Pin) from a waypoint - Returns even if currently disabled.
        /// <param name="oObject">The Waypoint object</param>
        /// <param name="nID">The Language ID (default English)</param>
        /// <param name="nGender">0 = Male, 1 = Female</param>
        public static string GetMapNote(uint oObject, int nID = 0, int nGender = 0)
        {
            NWNXPushInt(nGender);
            NWNXPushInt(nID);
            NWNXPushObject(oObject);
            NWNXCall(NWNX_Object, "GetMapNote");
            return NWNXPopString();
        }

        /// Sets a Map Note (AKA Map Pin) to any waypoint, even if no previous map note. Only updates for clients on area-load. Use SetMapPinEnabled() as required.
        /// <param name="oObject">The Waypoint object</param>
        /// <param name="sMapNote">The contents to set as the Map Note.</param>
        /// <param name="nID">The Language ID (default English)</param>
        /// <param name="nGender">0 = Male, 1 = Female</param>
        public static void SetMapNote(uint oObject, string sMapNote, int nID = 0, int nGender = 0)
        {
            NWNXPushInt(nGender);
            NWNXPushInt(nID);
            NWNXPushString(sMapNote);
            NWNXPushObject(oObject);
            NWNXCall(NWNX_Object, "SetMapNote");
        }

        /// Gets the last spell cast feat of oObject.
        /// @note Should be called in a spell script.
        /// <param name="oObject">The object.</param>
        /// <returns>The feat ID, or 65535 when not cast by a feat, or -1 on error.</returns>
        public static int GetLastSpellCastFeat(uint oObject)
        {
            NWNXPushObject(oObject);
            NWNXCall(NWNX_Object, "GetLastSpellCastFeat");
            return NWNXPopInt();
        }

        /// Sets the last object that triggered door or placeable trap.
        /// @note Should be retrieved with GetEnteringObject.
        /// <param name="oObject">Door or placeable object</param>
        /// <param name="oLast">Object that last triggered trap.</param>
        public static void SetLastTriggered(uint oObject, uint oLast)
        {
            NWNXPushObject(oLast);
            NWNXPushObject(oObject);
            NWNXCall(NWNX_Object, "SetLastTriggered");
        }

        /// Gets the remaining duration of the AoE object.
        /// <param name="oAoE">The AreaOfEffect object.</param>
        /// <returns>The remaining duration, in seconds, or the zero on failure.</returns>
        public static float GetAoEObjectDurationRemaining(uint oAoE)
        {
            NWNXPushObject(oAoE);
            NWNXCall(NWNX_Object, "GetAoEObjectDurationRemaining");
            return NWNXPopFloat();
        }

        /// Sets conversations started by oObject to be private or not.
        /// @note ActionStartConversation()&apos;s bPrivateConversation parameter will overwrite this flag.
        /// <param name="oObject">The object.</param>
        /// <param name="bPrivate">TRUE/FALSE.</param>
        public static void SetConversationPrivate(uint oObject, int bPrivate)
        {
            NWNXPushInt(bPrivate);
            NWNXPushObject(oObject);
            NWNXCall(NWNX_Object, "SetConversationPrivate");
        }

        /// Sets the radius of a circle AoE object.
        /// <param name="oAoE">The AreaOfEffect object.</param>
        /// <param name="fRadius">The radius, must be bigger than 0.0f.</param>
        public static void SetAoEObjectRadius(uint oAoE, float fRadius)
        {
            NWNXPushFloat(fRadius);
            NWNXPushObject(oAoE);
            NWNXCall(NWNX_Object, "SetAoEObjectRadius");
        }

        /// Gets the radius of a circle AoE object.
        /// <param name="oAoE">The AreaOfEffect object.</param>
        /// <returns>The radius or 0.0f on error</returns>
        public static float GetAoEObjectRadius(uint oAoE)
        {
            NWNXPushObject(oAoE);
            NWNXCall(NWNX_Object, "GetAoEObjectRadius");
            return NWNXPopFloat();
        }

        /// Gets whether the last spell cast of oObject was spontaneous.
        /// @note Should be called in a spell script.
        /// <param name="oObject">The object.</param>
        /// <returns>true if the last spell was cast spontaneously</returns>
        public static int GetLastSpellCastSpontaneous(uint oObject)
        {
            NWNXPushObject(oObject);
            NWNXCall(NWNX_Object, "GetLastSpellCastSpontaneous");
            return NWNXPopInt();
        }

        /// Gets the last spell cast domain level.
        /// @note Should be called in a spell script.
        /// <param name="oObject">The object.</param>
        /// <returns>Domain level of the cast spell, 0 if not a domain spell</returns>
        public static int GetLastSpellCastDomainLevel(uint oObject)
        {
            NWNXPushObject(oObject);
            NWNXCall(NWNX_Object, "GetLastSpellCastDomainLevel");
            return NWNXPopInt();
        }

        /// Force the given object to carry the given UUID. Any other object currently owning the UUID is stripped of it.
        /// <param name="oObject">The object</param>
        /// <param name="sUUID">The UUID to force</param>
        public static void ForceAssignUUID(uint oObject, string sUUID)
        {
            NWNXPushString(sUUID);
            NWNXPushObject(oObject);
            NWNXCall(NWNX_Object, "ForceAssignUUID");
        }

        /// Returns how many items are in oObject&apos;s inventory.
        /// <param name="oObject">A creature, placeable, item or store.</param>
        /// <returns>Returns a count of how many items are in oObject&apos;s inventory.</returns>
        public static int GetInventoryItemCount(uint oObject)
        {
            NWNXPushObject(oObject);
            NWNXCall(NWNX_Object, "GetInventoryItemCount");
            return NWNXPopInt();
        }

        /// Override the projectile visual effect of ranged/throwing weapons and spells.
        /// <param name="oCreature">The creature.</param>
        /// <param name="nProjectileType">A @ref projectile_types &quot;NWNX_OBJECT_SPELL_PROJECTILE_TYPE_*&quot; constant or -1 to remove the override.</param>
        /// <param name="nProjectilePathType">A &quot;PROJECTILE_PATH_TYPE_*&quot; constant or -1 to ignore.</param>
        /// <param name="nSpellID">A &quot;SPELL_*&quot; constant. -1 to ignore.</param>
        /// <param name="bPersist">Whether the override should persist to the .bic file (for PCs).</param>
        /// @note Persistence is enabled after a server reset by the first use of this function. Recommended to trigger on a dummy target OnModuleLoad to enable persistence.
        ///       This will override all spell projectile VFX from oCreature until the override is removed.
        public static void OverrideSpellProjectileVFX(uint oCreature, int nProjectileType = -1, int nProjectilePathType = -1, int nSpellID = -1, int bPersist = 0)
        {
            NWNXPushInt(bPersist);
            NWNXPushInt(nSpellID);
            NWNXPushInt(nProjectilePathType);
            NWNXPushInt(nProjectileType);
            NWNXPushObject(oCreature);
            NWNXCall(NWNX_Object, "OverrideSpellProjectileVFX");
        }

        /// Returns TRUE if the last spell was cast instantly. This function should only be called in a spell script.
        /// @note To initialize the hooks used by this function it is recommended to call this function once in your module load script.
        /// <returns>TRUE if the last spell was instant.</returns>
        public static int GetLastSpellInstant()
        {
            NWNXCall(NWNX_Object, "GetLastSpellInstant");
            return NWNXPopInt();
        }

        /// Sets the creator of a trap on door, placeable, or trigger. Also changes trap Faction to that of the new Creator.
        /// @note Triggers (ground traps) will instantly update colour (Green/Red). Placeable/doors will not change if client has already seen them.
        /// <param name="oObject">Door, placeable or trigger (trap) object</param>
        /// <param name="oCreator">The new creator of the trap. Any non-creature creator will assign OBJECT_INVALID (similar to toolset-laid traps)</param>
        public static void SetTrapCreator(uint oObject, uint oCreator)
        {
            NWNXPushObject(oCreator);
            NWNXPushObject(oObject);
            NWNXCall(NWNX_Object, "SetTrapCreator");
        }

        /// Return the name of the object for nLanguage.
        /// <param name="oObject">an object</param>
        /// <param name="nLanguage">A PLAYER_LANGUAGE constant.</param>
        /// <param name="nGender">  Gender to use, 0 or 1.</param>
        /// <returns>The localized string.</returns>
        public static string GetLocalizedName(uint oObject, int nLanguage, int nGender = 0)
        {
            const string sFunc = "GetLocalizedName";
            NWNXPushInt(nGender);
            NWNXPushInt(nLanguage);
            NWNXPushObject(oObject);
            NWNXCall(NWNX_Object, sFunc);
            return NWNXPopString();
        }

        /// Set the name of the object as set in the toolset for nLanguage.
        /// @note You may have to SetName(oObject, &quot;&quot;) for the translated string to show.
        /// <param name="oObject">an object</param>
        /// <param name="sName">New value to set</param>
        /// <param name="nLanguage">A PLAYER_LANGUAGE constant.</param>
        /// <param name="nGender">  Gender to use, 0 or 1.</param>
        public static void SetLocalizedName(uint oObject, string sName, int nLanguage, int nGender = 0)
        {
            const string sFunc = "SetLocalizedName";
            NWNXPushInt(nGender);
            NWNXPushInt(nLanguage);
            NWNXPushString(sName);
            NWNXPushObject(oObject);
            NWNXCall(NWNX_Object, sFunc);
        }

        // @}
    }

    public struct LocalVariable
    {
        public int type;
        public string key;
    }
}