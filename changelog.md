# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

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


