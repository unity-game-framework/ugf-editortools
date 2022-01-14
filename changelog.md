# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [2.3.0](https://github.com/unity-game-framework/ugf-editortools/releases/tag/2.3.0) - 2022-01-14  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-editortools/milestone/40?closed=1)  
    

### Added

- Add text field with dropdown ([#203](https://github.com/unity-game-framework/ugf-editortools/issues/203))  
    - Add `EditorElementsUtility.TextFieldWithDropdown()` method to draw text field with dropdown to select possible values.

## [2.2.0](https://github.com/unity-game-framework/ugf-editortools/releases/tag/2.2.0) - 2021-12-22  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-editortools/milestone/39?closed=1)  
    

### Added

- Add assembly selection dropdown ([#200](https://github.com/unity-game-framework/ugf-editortools/pull/200))  
    - Add `AssemblyDropdownDrawer` class to draw dropdown with assembly selection.
    - Add `AssemblyReference` structure to handle assembly full name as reference.
    - Add `AssemblyReferenceDropdownAttribute` attribute class to specify drawing of dropdown for properties of `AssemblyReference` type.
    - Add `AssemblyUtility` class with static methods to work with assemblies.
- Add handlers draw wire capsule ([#198](https://github.com/unity-game-framework/ugf-editortools/pull/198))  
    - Add `HandleEditorUtility.DrawWireCapsule()` method to draw wire capsule using editor handles.

### Fixed

- Fix tool handler position scale issue ([#197](https://github.com/unity-game-framework/ugf-editortools/pull/197))  
    - Fix `ToolComponentHandlePosition` class to properly draw position handler with scaled object.

## [2.1.0](https://github.com/unity-game-framework/ugf-editortools/releases/tag/2.1.0) - 2021-11-23  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-editortools/milestone/38?closed=1)  
    

### Added

- Add shape scene handler drawers ([#191](https://github.com/unity-game-framework/ugf-editortools/pull/191))  
    - Add `ToolComponent` abstract class to implement _Editor Tool_ bounded to component.
    - Add `ToolComponentHandle` abstract class as base class for _Editor Tool_ used with in-scene handles.
    - Add `ToolComponentBoundsHandle<T>` abstract class and multiple implementations to support creating _Editor Tools_ for in scene shape handlers, such as _Box_, _Capsule_ and _Sphere_.
    - Add `ToolComponentHandlePosition` class as implementation for in-scene handle used to modify position value.
- Add reorderable list of object references with selection preview ([#190](https://github.com/unity-game-framework/ugf-editortools/pull/190))  
    - Add `ReorderableListSelectionDrawer` abstract class to implement selection of `ReorderableListDrawer` with preview.
    - Add `ReorderableListSelectionDrawerByElement` class to draw selection of `ReorderableListDrawer` using object reference value directly from element.
    - Add `ReorderableListSelectionDrawerByPath` class to draw selection of `ReorderableListDrawer` using object reference from serialized property accessed by specified path from element.
- Add reorderable list events ([#188](https://github.com/unity-game-framework/ugf-editortools/pull/188))  
    - Add `ReorderableListDrawer.Added`, `Removed`, `Selected` and `SelectionUpdated` events.
    - Add `ReorderableListDrawer.OnSelectionUpdate()` method.
- Add PackageEditorUtility ([#186](https://github.com/unity-game-framework/ugf-editortools/pull/186))  
    - Add `PackageEditorUtility.TryGetPackage()` method to get `PackageInfo` class with information about package from _Package Manager_ by the specified package name.

### Fixed

- Fix collection drawer element spacing ([#189](https://github.com/unity-game-framework/ugf-editortools/pull/189))  
    - Fix `CollectionDrawer` element spacing drawing.

## [2.0.0](https://github.com/unity-game-framework/ugf-editortools/releases/tag/2.0.0) - 2021-11-04  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-editortools/milestone/36?closed=1)  
    

### Changed

- Add FromYamlAtPath with specified type ([#182](https://github.com/unity-game-framework/ugf-editortools/pull/182))  
    - Add `EditorYamlUtility.TryFromYamlAtPath()` and `FromYamlAtPath()` methods to get object from _Yaml_ by the specified target type.
    - Deprecate `EditorYamlUtility.FromYaml()` and `FromYamlAtPath()` methods without specified type.

## [2.0.0-preview.5](https://github.com/unity-game-framework/ugf-editortools/releases/tag/2.0.0-preview.5) - 2021-09-04  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-editortools/milestone/34?closed=1)  
    

### Fixed

- Fix PlatformSettingsExtensions incorrect access for settings ([#178](https://github.com/unity-game-framework/ugf-editortools/pull/178))  
    - Fix `PlatformSettingsDrawer` and `PlatformSettingsExtensions` classes to work using platform information from `PlatformEditorUtility` class.

## [2.0.0-preview.4](https://github.com/unity-game-framework/ugf-editortools/releases/tag/2.0.0-preview.4) - 2021-08-23  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-editortools/milestone/32?closed=1)  
    

### Added

- Add indent support for group settings drawer ([#173](https://github.com/unity-game-framework/ugf-editortools/pull/173))  
    - Add `SettingsGroupsDrawer` ability to use indent in current scope.
- Add platform settings to display platform name ([#170](https://github.com/unity-game-framework/ugf-editortools/pull/170))  
    - Add `PlatformSettingsDrawer.DisplayPlatformName` property to determine whether to display platform name as label on top of the settings data.
    - Add `PlatformSettingsDrawer.OnDrawSettingsPlatformName` protected virtual method to override platform display.
    - Add `PlatformEditorUtility.TryGetPlatform()` and `GetPlatform()` methods to get platform info by platform name.

### Fixed

- Fix reset GUI selection when tab changed ([#172](https://github.com/unity-game-framework/ugf-editortools/pull/172))  
    - Fix `SettingsGroupsDrawer` to reset GUI selection when change tab.

## [2.0.0-preview.3](https://github.com/unity-game-framework/ugf-editortools/releases/tag/2.0.0-preview.3) - 2021-08-02  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-editortools/milestone/30?closed=1)  
    

### Added

- Add preference value containers ([#164](https://github.com/unity-game-framework/ugf-editortools/pull/164))  
    - Add `PreferenceEditorValue<T>` class to manage value that stored at editor preferences.
    - Add `PreferencesEditorUtility` class to work with any type of value that stored at editor preferences.

## [2.0.0-preview.2](https://github.com/unity-game-framework/ugf-editortools/releases/tag/2.0.0-preview.2) - 2021-07-24  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-editortools/milestone/29?closed=1)  
    

### Added

- Add serialized object shortcut to get script property ([#159](https://github.com/unity-game-framework/ugf-editortools/pull/159))  
    - Add `EditorIMGUIUtility.DrawScriptProperty()` method to draw _Script_ property of a serialized object.
    - Add `EditorIMGUIUtility.GetScriptProperty()` method to get _Script_ property of a serialized object.

## [2.0.0-preview.1](https://github.com/unity-game-framework/ugf-editortools/releases/tag/2.0.0-preview.1) - 2021-07-03  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-editortools/milestone/28?closed=1)  
    

### Fixed

- Fix PlatformSettingsDrawer to work with platforms from Unity 2021.2 ([#157](https://github.com/unity-game-framework/ugf-editortools/pull/157))  
    - Add `PlatformSettingsDrawer.AddPlatform()` method to add platform information with specified `PlatformInfo` as argument.
    - Fix `PlatformSettingsDrawer.AddPlatformAllAvailable()` and `AddPlatformAll()` methods to initialize platform information using `PlatformInfo` directly.

## [2.0.0-preview](https://github.com/unity-game-framework/ugf-editortools/releases/tag/2.0.0-preview) - 2021-07-03  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-editortools/milestone/27?closed=1)  
    

### Changed

- Fix PlatformEditorUtility throws exception on creation with Unity 2021.2 ([#154](https://github.com/unity-game-framework/ugf-editortools/pull/154))  
    - Change package _Unity_ version to `2021.2`.
    - Fix `PlatformEditorUtility` throws exceptions on initialization of internal platforms information from the editor.
    - Remove `PlatformSettingsEditorUtility` class as deprecated.
    - Remove `ManagedReferenceEditorUtility.GetTypeItems()` method as deprecated.
    - Remove `TypesDropdownEditorUtility.GetTypeItems()` and `CreateItem()` methods as deprecated.

## [1.11.1](https://github.com/unity-game-framework/ugf-editortools/releases/tag/1.11.1) - 2021-06-03  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-editortools/milestone/26?closed=1)  
    

### Fixed

- Fix pages collection drawer error when array is zero ([#152](https://github.com/unity-game-framework/ugf-editortools/pull/152))  
    - Fix error when page index more than page max index.
    - Change page field to display only when page count more than two.

## [1.11.0](https://github.com/unity-game-framework/ugf-editortools/releases/tag/1.11.0) - 2021-02-09  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-editortools/milestone/25?closed=1)  
    

### Added

- Add gameobject tag dropdown drawer ([#144](https://github.com/unity-game-framework/ugf-editortools/pull/144))  
    - Add `TagDropdownAttribute` to draw _Unity_ tag dropdown for fields with `string` value.

### Changed

- Update project registry ([#145](https://github.com/unity-game-framework/ugf-editortools/pull/145))  
    - Update package publish registry.

## [1.10.0](https://github.com/unity-game-framework/ugf-editortools/releases/tag/1.10.0) - 2021-01-24  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-editortools/milestone/24?closed=1)  
    

### Added

- Add build platforms editor utilities ([#139](https://github.com/unity-game-framework/ugf-editortools/pull/139))  
    - Add `PlatformEditorUtility` class to provide full platforms information.
    - Deprecate `PlatformSettingsEditorUtility` class, use `PlatformEditorUtility` platforms information instead.

### Fixed

- Fix unknown build target detected when get all available platforms ([#138](https://github.com/unity-game-framework/ugf-editortools/pull/138))  
    - Fix `PlatformSettingsEditorUtility.GetBuildTarget()` method to work when editor update platform build target list.

## [1.9.0](https://github.com/unity-game-framework/ugf-editortools/releases/tag/1.9.0) - 2021-01-16  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-editortools/milestone/23?closed=1)  
    

### Added

- Add collection and pages collection drawers ([#132](https://github.com/unity-game-framework/ugf-editortools/pull/132))  
    - Add `CollectionDrawer` class to draw simple serializable collections.
    - Add `PagesCollectionDrawer` class to draw simple serializable collections with pages.

## [1.8.1](https://github.com/unity-game-framework/ugf-editortools/releases/tag/1.8.1) - 2020-12-15  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-editortools/milestone/22?closed=1)  
    

### Added

- Add GlobalId generate ([#130](https://github.com/unity-game-framework/ugf-editortools/pull/130))  
    - Add `GlobalId.Generate()` static method to create instance with new generated guid data.

## [1.8.0](https://github.com/unity-game-framework/ugf-editortools/releases/tag/1.8.0) - 2020-12-07  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-editortools/milestone/21?closed=1)  
    

### Added

- Add global id container ([#127](https://github.com/unity-game-framework/ugf-editortools/pull/127))  
    - Add `GlobalId` structure to store serialized data of `Guid`, with abilities to convert vice versa.
    - Add `GlobalIdEditorUtility` class with utilities to work with `GlobalId` at editor.
    - Add `AssetIdAttribute` to draw field of `GlobalId` as object field for asset of specific type.
    - Add `AssetIdReference<T>` structure, same as `AssetReference<T>` but asset guid stored as `GlobalId`.
    - Add `AssetIdReferenceEditorGUIUtility` class with utilities to draw field of `AssetIdReference<T>`.
    - Add `AssetIdReferenceListDrawer` list drawer to use with list of `AssetIdReference<T>`.

## [1.7.0](https://github.com/unity-game-framework/ugf-editortools/releases/tag/1.7.0) - 2020-11-19  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-editortools/milestone/20?closed=1)  
    

### Added

- Add TypeReference serializable container for type with dropdown selection ([#123](https://github.com/unity-game-framework/ugf-editortools/pull/123))  
    - Add `TypeReference<T>` to store and select type using dropdown.
    - Add `TypeReferenceDropdownAttribute` attribute to control `TypeReference<T>` dropdown display.
    - Add `TypesDropdownAttributeBase` abstract attribute as base attribute used to display types dropdown.
    - Change `TypesDropdownAttributePropertyDrawer<T>` target attribute type to `TypesDropdownAttributeBase` attribute.
- Add ability to extend types dropdown attribute display behaviour ([#122](https://github.com/unity-game-framework/ugf-editortools/pull/122))  
    - Add `TypesDropdownAttributePropertyDrawer<T>` abstract class to implement custom types dropdown property drawer.
    - Add `TypesDropdownDrawer.OnGetContentDisplayText` method to override display of current selected type.
    - Add `TypesDropdownEditorUtility.GetTypeItems` overload method with type validation function.
    - Add `ManagedReferenceAttribute.DisplayAssemblyName` property to determine whether to display assembly name within type name.
    - Change `ManagedReferenceEditorUtility.GetTypeItems` method to be deprecated, use `TypesDropdownEditorUtility.GetTypeItems` instead.

### Fixed

- Fix types dropdown incorrect nested type name display ([#121](https://github.com/unity-game-framework/ugf-editortools/pull/121))  
    - Fix types dropdown displays only short name of nested types for item and current selected value.
    - Add `TypesDropdownAttribute.DisplayAssemblyName` property to determine whether to display assembly name within type name.
    - Add `TypesDropdownEditorUtility.GetTypeDisplayName` to get type name with consideration of nested types and assembly name.
    - Change `TypesDropdownEditorUtility.GetTypeItems` to be deprecated, use method overload with collection and display assembly name arguments.
    - Change `TypesDropdownEditorUtility.CreateItem` to be deprecated, use method overload with display assembly name argument.

## [1.6.0](https://github.com/unity-game-framework/ugf-editortools/releases/tag/1.6.0) - 2020-10-26  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-editortools/milestone/19?closed=1)  
    

### Added

- Add SerializedObject scope to update and apply properties ([#114](https://github.com/unity-game-framework/ugf-editortools/pull/114))  
    - Add `SerializedObjectUpdateScope` disposable scope to write code between serializedobject update and apply.

### Changed

- Change reorderable list to not display label when element has no visible children ([#113](https://github.com/unity-game-framework/ugf-editortools/pull/113))  
    - Change to display label only when element has visible children or it is empty reference.

### Removed

- Remove deprecated code ([#115](https://github.com/unity-game-framework/ugf-editortools/pull/115))  
    - Remove `PlatformSettingsPropertyDrawerBase.OnDrawerSettingsCreated` method.
    - Remove `PlatformSettingsEditorUtility.GetPlatformsAvailable` and `PlatformSettingsEditorUtility.GetPlatformsAll` methods which works with `BuildTargetGroup` enumerable.
    - Remove `EditorIMGUIUtility.DrawAssetGuidField` and `EditorIMGUIUtility.DrawAssetGuidField` methods.

### Fixed

- Fix DrawResourcesPathField throws error when select none ([#111](https://github.com/unity-game-framework/ugf-editortools/pull/111))

## [1.5.2](https://github.com/unity-game-framework/ugf-editortools/releases/tag/1.5.2) - 2020-10-24  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-editortools/milestone/18?closed=1)  
    

### Added

- Add AssetReference validation ([#107](https://github.com/unity-game-framework/ugf-editortools/pull/107))  
    - Add `HasGuid` and `HasAsset` properties.
    - Add `IsValid` to validate asset reference data.
    - Change `Guid` and `Asset` properties to throw exceptions when data is invalid.

### Fixed

- Fix managed reference dropdown not display types to select ([#106](https://github.com/unity-game-framework/ugf-editortools/pull/106))

## [1.5.1](https://github.com/unity-game-framework/ugf-editortools/releases/tag/1.5.1) - 2020-10-22  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-editortools/milestone/17?closed=1)  
    

### Fixed

- Fix AssetReferencePropertyDrawer do not have height override ([#102](https://github.com/unity-game-framework/ugf-editortools/pull/102))

## [1.5.0](https://github.com/unity-game-framework/ugf-editortools/releases/tag/1.5.0) - 2020-10-21  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-editortools/milestone/16?closed=1)  
    

### Added

- Add AssetReference property to store asset guid and asset ([#98](https://github.com/unity-game-framework/ugf-editortools/pull/98))  
    - Add `AssetReference<T>` to store asset reference with asset guid at runtime.
    - Add `AssetReferenceEditorGUIUtility` to draw `AssetReference<T>` property in editor.
    - Add `AssetReferenceListDrawer` to draw list of `AssetReference<T>` properties.

### Fixed

- Fix elements indent in EnabledPropertyListDrawer ([#99](https://github.com/unity-game-framework/ugf-editortools/pull/99))  
    - Fix `EnabledPropertyListDrawer` to not display additional indent where not required.
    - Add `ReorderableListDrawer.OnElementHasVisibleChildren` overridable method to determine whether element should be indented to display foldout button.

## [1.4.1](https://github.com/unity-game-framework/ugf-editortools/releases/tag/1.4.1) - 2020-10-20  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-editortools/milestone/15?closed=1)  
    

### Fixed

- Fix EditorDrawer do not dispose created editor on disable ([#95](https://github.com/unity-game-framework/ugf-editortools/pull/95))

## [1.4.0](https://github.com/unity-game-framework/ugf-editortools/releases/tag/1.4.0) - 2020-10-19  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-editortools/milestone/14?closed=1)  
    

### Added

- Add check for MonoScript existence during editor yaml serialization ([#91](https://github.com/unity-game-framework/ugf-editortools/pull/91))  
    - Add `EditorYamlUtility.ValidateForDeserialization` to determines whether specified object has properly defined script file.
    - Add additional parameter `validate` for all serialize to yaml methods of `EditorYamlUtility` class, to determines whether specified targets can be properly deserialized later.

### Changed

- Update to Unity 2020.2 ([#89](https://github.com/unity-game-framework/ugf-editortools/pull/89))  

### Fixed

- Fix EditorDrawer use editor object to display titlebar instead of target ([#90](https://github.com/unity-game-framework/ugf-editortools/pull/90))

## [1.3.1](https://github.com/unity-game-framework/ugf-editortools/releases/tag/1.3.1) - 2020-10-04  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-editortools/milestone/13?closed=1)  
    

### Added

- Add editor PlatformSettings extensions for access to specific settings ([#84](https://github.com/unity-game-framework/ugf-editortools/pull/84))  
    - Add `PlatformSettingsExtensions` with methods used to access to settings by `BuildTargetGroup`.

## [1.3.0](https://github.com/unity-game-framework/ugf-editortools/releases/tag/1.3.0) - 2020-10-03  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-editortools/milestone/12?closed=1)  
    

### Added

- Add automatic setttings object instance creation ([#78](https://github.com/unity-game-framework/ugf-editortools/pull/78))  
    - Add `SettingsGroupsWithTypesDrawer` with types collection for each group to automatically create on demand.
    - Add `PlatformSettingsDrawer.AutoSettingsInstanceCreation` to determine whether to automatically create object instance of required settings.
    - Add `SettingsGroupsDrawer.TryGetGroupLabel` method to get label by group name.
    - Add `OnGroupAdded`, `OnGroupRemoved` and `OnGroupsCleared` virtual methods for `SettingsGroupsDrawer` class.
    - Change `PlatformSettingsDrawer` inherit from `SettingsGroupsWithTypesDrawer` to support group types.
    - Change `PlatformSettingsPropertyDrawer` to be public class with default implementation to draw properties of `PlatformSettings<T>` type.
    - Deprecate `PlatformSettingsPropertyDrawerBase.OnDrawerSettingsCreated` method, which no longer used and will be removed, use `Drawer.SettingsCreated` event instead, when settings creation override required.
    - Deprecate `PlatformSettingsEditorUtility.GetPlatformsAvailable` and `PlatformSettingsEditorUtility.GetPlatformsAll` methods, use `BuildTargetGroupsAll` and `BuildTargetGroupsAllAvailable` properties instead.
- Add PlatformSettingsDrawer.SettingsDrawing event to override drawing ([#77](https://github.com/unity-game-framework/ugf-editortools/pull/77))  
    - Add `PlatformSettingsDrawer.SettingsDrawing` event which called when selected settings drawing.
    - Add `SettingsGroupsDrawer.AllowEmptySettings` to determine whether empty settings allowed, otherwise force to create settings object through the `OnCreateSettings` method or `SettingsCreated` event.
    - Change `PlatformSettingsPropertyDrawerBase.Drawer` set accessor to be public.
    - Change `PlatformSettingsDrawer.SettingsCreated` event type, with `SettingsCreatedHandler` to specify parameters meaning.
    - ~~Fix `PlatformSettingsPropertyDrawerBase` not subscribed on `Drawer.SettingsCreated` event.~~ (Related: #78)
- Add methods to append all or only available platforms for PlatformSettingsDrawer ([#75](https://github.com/unity-game-framework/ugf-editortools/pull/75))  
    - Add `PlatformSettingsDrawer.AddPlatformAllAvailable` method to add all available platforms to draw.
    - Add `PlatformSettingsDrawer.AddPlatformAll` method to add all known platforms to draw.
    - Add `PlatformSettingsDrawer.AddPlatformAll` method overload to add platforms to display form specified list.
    - Change `PlatformSettingsPropertyDrawer` to add all platforms to drawer by default.
- Add EnabledProperty equality operators ([#74](https://github.com/unity-game-framework/ugf-editortools/pull/74))  
    - Add `==` and `!=` operators to compare property value only.
    - Add implicit operator to convert property to `boolean`, which can be used to check whether property enabled.

### Deprecated

- Deprecate PlatformSettingsPropertyDrawerBase.OnDrawerSettingsCreated method ([#80](https://github.com/unity-game-framework/ugf-editortools/issues/80))  
    - `PlatformSettingsPropertyDrawerBase.OnDrawerSettingsCreated` method no longer used and will be removed, use `Drawer.SettingsCreated` event instead, when settings creation override required.
- Deprecate PlatformSettingsEditorUtility.GetPlatformsAvailable and GetPlatformsAll methods ([#79](https://github.com/unity-game-framework/ugf-editortools/issues/79))  
    - Deprecate `PlatformSettingsEditorUtility.GetPlatformsAvailable` and `PlatformSettingsEditorUtility.GetPlatformsAll` methods, use `BuildTargetGroupsAll` and `BuildTargetGroupsAllAvailable` properties instead.

### Fixed

- Fix ManagedReferenceAttribute dropdown selection display only derived types ([#76](https://github.com/unity-game-framework/ugf-editortools/pull/76))  
    - Fix `ManagedReferenceEditorUtility.GetTypeItems` to return all types assignable from specified target type.
    - Fix `ManagedReferenceEditorUtility.IsValidType` to validate only types of classes.

## [1.2.0](https://github.com/unity-game-framework/ugf-editortools/releases/tag/1.2.0) - 2020-09-30  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-editortools/milestone/11?closed=1)  
    

### Added

- Add editor and runtime platform settings selection ([#64](https://github.com/unity-game-framework/ugf-editortools/pull/64))  
    - Add `PlatformSettings` to store settings by `BuildTargetGroup`.
    - Add `PlatformSettingsDrawer` to draw platform selection group with toolbar, with default implementation for `PlatformSettings`.
    - Add `SettingsGroups` with collection of `SettingsGroup` to store groups with settings of any type, using `SerializeReference`.
    - Add `SettingsGroupsDrawer` to draw `SettingsGroups` with toolbar group selection.
    - Add `ToolbarDrawer` to draw horizonal or vertical toolbars.
    - Add `PlatformSettingsEditorUtility` with utilities to get information about build platforms, such as display name and icons.

## [1.1.0](https://github.com/unity-game-framework/ugf-editortools/releases/tag/1.1.0) - 2020-09-25  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-editortools/milestone/10?closed=1)  
    

### Added

- Add AssetPathAttribute and ResourcesPathAttribute  to assign path from asset reference ([#58](https://github.com/unity-game-framework/ugf-editortools/pull/58))  
    - Add `AssetPathAttribute` used to mark field of `string` type to display as object field which select asset full path.
    - Add `ResourcesPathAttribute` used to mark field of `string` type to display as object field which select asset resources path.
    - Add `AttributeEditorGUIUtility` which replaces `DrawAssetGuidField` method and overloads from `EditorIMGUIUtility`, and they become deprecated.
    - Add `AttributeEditorGUIUtility.DrawAssetPathField` to draw object field which select asset path.
    - Add `EditorIMGUIUtility.MissingObject` which can be used to display "Missing" object in object field.
    - Add `EditorIMGUIUtility.IsMissingObject` to determine whether specified object is `EditorIMGUIUtility.MissingObject`.
    - Add `AssetsEditorUtility.TryGetResourcesPath` method used to get asset resources path.
    - Add `AssetsEditorUtility.TryGetResourcesRelativePath` method used to convert full asset path to resources relative path.
    - Add `AttributeEditorGUIUtility.DrawResourcesPathField` to draw object field which select asset resources path.
- Add enabled property ([#57](https://github.com/unity-game-framework/ugf-editortools/pull/57))  
    - Add `EnabledProperty<TValue>` structure to store any value with `Enabled` property.
    - Add `EnabledPropertyDrawer` and `EnabledPropertyGUIUtility` to draw default and custom enabled property.
    - Add `EnabledPropertyListDrawer` to draw reorderable list of `EnabledProperty<TValue>` structure.
    - Add `PropertyDrawerBase` as default implementation of `PropertyDrawer`, and change `PropertyDrawer<TAttribute>` to inherit from it.
    - Add `LabelWidthChangeScope` to change editor GUI label width inside of scope.
    - Add `EditorIMGUIUtility.IndentPerLevel` property to access editor internal `kIndentPerLevel` value.
    - Add `EditorIMGUIUtility.GetIndentWithLevel` method to calculate indent with specific indent level.
- Add compile defines controls ([#53](https://github.com/unity-game-framework/ugf-editortools/pull/53))  
    - Add `DefinesEditorUtility` with methods to change compile defines per platform.
- Add DrawerBase as default drawer class ([#50](https://github.com/unity-game-framework/ugf-editortools/pull/50))  
    - Add `DrawerBase` with `Enable` and `Disable` methods.
    - Change some other drawers to inherit `DrawerBase`.

### Changed

- Change EditorDrawer.DisplayTitlebar to false by default ([#51](https://github.com/unity-game-framework/ugf-editortools/pull/51))  

### Deprecated

- Deprecate EditorIMGUIUtility.DrawAssetGuidField methods ([#59](https://github.com/unity-game-framework/ugf-editortools/issues/59))  
    - Methods relocated to `AttributeEditorGUIUtility` class, use them instead.

## [1.0.0](https://github.com/unity-game-framework/ugf-editortools/releases/tag/1.0.0) - 2020-09-18  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-editortools/milestone/9?closed=1)  
    

### Added

- Add editor object serialization ([#45](https://github.com/unity-game-framework/ugf-editortools/pull/45))  
- Add misc editor GUI scopes ([#43](https://github.com/unity-game-framework/ugf-editortools/pull/43))  
    - Add `IndentLevelScope` to set indent level value inside of scope.
    - Add `IndentIncrementScope` to increment indent level by value inside of scope.
    - Add `GUIColorScope` to change GUI color inside of scope.
    - Add `GUIBackgroundColorScope` to change GUI background color inside of scope.
- Add InspectorGUIScope with GUI setup same as in inspector window ([#42](https://github.com/unity-game-framework/ugf-editortools/pull/42))  
    Add `InspectorGUIScope` to setup `hierarchyMode`, `wideMode`, `labelWidth` and default one increment of indent level.
- Add DisabledAttribute to disable field in inspector normal mode ([#40](https://github.com/unity-game-framework/ugf-editortools/pull/40))  
- Add Reorderable collection with editors ([#36](https://github.com/unity-game-framework/ugf-editortools/pull/36))  
    - Add `ReorderableListDrawer` as default implementation of `ReorderableList`.
    - Add `EditorIMGUIUtility.GetIndent` to access internal editor indent width.
    - Add `LabelWidthScope` to control editor GUI label width.
    - Add `EditorDrawer` and `EditorObjectReferenceDrawer` to draw editor of selected target
    - Add `EditorListDrawer` to draw list and editor for target from selected element.
- Add SerializeReference selection attribute ([#35](https://github.com/unity-game-framework/ugf-editortools/pull/35))  
    - Add `ManagedReference` attribute to allow selection of new value of specific type for fields defined with `SerializeReference` attribute.
- Add field attribute property drawer ([#33](https://github.com/unity-game-framework/ugf-editortools/pull/33))  
    - Add `PropertyDrawer<T>` where `T` is type of attribute.
    - Add `PropertyDrawerTyped<T>` with constraints for type of serialized property.
- Add Generic AdvancedDropdown ([#32](https://github.com/unity-game-framework/ugf-editortools/pull/32))  
    - Add `Dropdown<TItem>` with other elements to implement custom searchable dropdowns.
    - Add `DropdownEditorGUIUtility` with methods to draw dropdown and related elements.
- Add access to editor GUI last control id ([#30](https://github.com/unity-game-framework/ugf-editortools/issues/30))  
    - Add `EditorIMGUIUtility.GetLastControlId` to access to the last GUI control id.

### Changed

- Change location for some classes ([#39](https://github.com/unity-game-framework/ugf-editortools/pull/39))  
    - Move attributes classes under `Attributes` folder.
    - Move scope classes under `Scopes` folder.
- Rework TypesDropdown to use new dropdown ([#34](https://github.com/unity-game-framework/ugf-editortools/pull/34))  
    - Rework to use new `Dropdown<T>` and `PropertyDrawer<T>` elements.

## [0.6.0-preview](https://github.com/unity-game-framework/ugf-editortools/releases/tag/0.6.0-preview) - 2020-02-15  

- [Commits](https://github.com/unity-game-framework/ugf-editortools/compare/0.6.0-preview...0.5.1-preview)
- [Milestone](https://github.com/unity-game-framework/ugf-editortools/milestone/8?closed=1)

### Added
- Add support for `SceneAsset` using `AssetGuidAttribute` via specifying `Scene` as asset type.
- Add `AssetTypeAttribute` to control type of the asset object field.

## [0.5.1-preview](https://github.com/unity-game-framework/ugf-editortools/releases/tag/0.5.1-preview) - 2020-02-02  

- [Commits](https://github.com/unity-game-framework/ugf-editortools/compare/0.5.0-preview...0.5.1-preview)
- [Milestone](https://github.com/unity-game-framework/ugf-editortools/milestone/7?closed=1)

### Added
- `EditorIMGUIUtility.DrawAssetGuidField` to draw asset field using guid.

### Fixed
- `AssetGuidAttribute` asset field indentation.

## [0.5.0-preview](https://github.com/unity-game-framework/ugf-editortools/releases/tag/0.5.0-preview) - 2020-02-02  

- [Commits](https://github.com/unity-game-framework/ugf-editortools/compare/0.4.0-preview...0.5.0-preview)
- [Milestone](https://github.com/unity-game-framework/ugf-editortools/milestone/6?closed=1)

### Added
- `AssetGuidAttribute` to mark string field as asset field and store asset guid.
- `TypesDropdownAttribute` add serialized property type validation.

## [0.4.0-preview](https://github.com/unity-game-framework/ugf-editortools/releases/tag/0.4.0-preview) - 2020-01-10  

- [Commits](https://github.com/unity-game-framework/ugf-editortools/compare/0.3.1-preview...0.4.0-preview)
- [Milestone](https://github.com/unity-game-framework/ugf-editortools/milestone/5?closed=1)

### Added
- `TypesDropdownAttribute` and `TypesDropdownAttributeDrawer`.

## [0.3.1-preview](https://github.com/unity-game-framework/ugf-editortools/releases/tag/0.3.1-preview) - 2020-01-09  

- [Commits](https://github.com/unity-game-framework/ugf-editortools/compare/0.3.0-preview...0.3.1-preview)
- [Milestone](https://github.com/unity-game-framework/ugf-editortools/milestone/4?closed=1)

### Fixed
- `TypesDropdown`: popup window size.

## [0.3.0-preview](https://github.com/unity-game-framework/ugf-editortools/releases/tag/0.3.0-preview) - 2020-01-07  

- [Commits](https://github.com/unity-game-framework/ugf-editortools/compare/0.2.0-preview...0.3.0-preview)
- [Milestone](https://github.com/unity-game-framework/ugf-editortools/milestone/3?closed=1)

### Added
- `EditorProgressbarScope`: scope to control editor progressbar.
- `TypesDropdown`: `None` item to select nothing.
- `EditorIMGUIUtility` with `DrawDefaultInspector` and `DrawSerializedPropertyChildren` methods.
- `EditorTempScope`: scope to control temp folder or files.

## [0.2.0-preview](https://github.com/unity-game-framework/ugf-editortools/releases/tag/0.2.0-preview) - 2019-11-09  

- [Commits](https://github.com/unity-game-framework/ugf-editortools/compare/0.1.0-preview...0.2.0-preview)
- [Milestone](https://github.com/unity-game-framework/ugf-editortools/milestone/2?closed=1)

### Changed
- Regenerate meta files.

## [0.1.0-preview](https://github.com/unity-game-framework/ugf-editortools/releases/tag/0.1.0-preview) - 2019-10-21  

- [Commits](https://github.com/unity-game-framework/ugf-editortools/compare/b21014c...0.1.0-preview)
- [Milestone](https://github.com/unity-game-framework/ugf-editortools/milestone/1?closed=1)

### Added
- This is a initial release.


