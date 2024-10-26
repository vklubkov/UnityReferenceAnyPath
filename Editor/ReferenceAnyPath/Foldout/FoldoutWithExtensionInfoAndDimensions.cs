using UnityEditor;

namespace ReferenceAnyPath {
    public class FoldoutWithExtensionInfoAndDimensions<TLayout> :
        FoldoutWithExtensionInfo<TLayout> where TLayout : IRawHeightmapLayout {
        const string _widthLabel = "Width";
        const string _heightLabel = "Height";
        const string _bitsLabel = "Bits";
        const string _byteOrderLabel = "Byte Order";
        const string _flipLabel = "Flip";

        public override int Lines => base.Lines + 4;

        protected override bool DrawFoldout() {
            DrawExtensionInfo();
            var hasChanges = DrawDimensionsInput();
            DrawDefaultInfo();
            return hasChanges;
        }

        bool DrawDimensionsInput() {
            Layout.NextLine();

            EditorGUI.LabelField(Layout.WidthLabelRect, _widthLabel);
            var widthProperty = Property.GetProperty(PropertyName._width);
            var originalWidth = widthProperty.intValue;
            EditorGUI.PropertyField(Layout.WidthValueRect, widthProperty, Layout.NoGuiContent);
            var hasChanges = originalWidth != widthProperty.intValue;

            EditorGUI.LabelField(Layout.HeightLabelRect, _heightLabel);
            var heightProperty = Property.GetProperty(PropertyName._height);
            var originalHeight = heightProperty.intValue;
            EditorGUI.PropertyField(Layout.HeightValueRect, heightProperty, Layout.NoGuiContent);
            hasChanges |= originalHeight != heightProperty.intValue;

            Layout.NextLine();

            EditorGUI.LabelField(Layout.BitsLabelRect, _bitsLabel);
            var bitsProperty = Property.GetProperty(PropertyName._bits);
            var originalBits = bitsProperty.intValue;
            EditorGUI.PropertyField(Layout.BitsValueRect, bitsProperty, Layout.NoGuiContent);
            hasChanges |= originalBits != bitsProperty.intValue;

            Layout.NextLine();

            EditorGUI.LabelField(Layout.ByteOrderLabelRect, _byteOrderLabel);
            var byteOrderProperty = Property.GetProperty(PropertyName._byteOrder);
            EditorGUI.PropertyField(Layout.ByteOrderValueRect, byteOrderProperty, Layout.NoGuiContent);

            Layout.NextLine();

            EditorGUI.LabelField(Layout.FlipLabelRect, _flipLabel);
            var flipProperty = Property.GetProperty(PropertyName._flip);
            EditorGUI.PropertyField(Layout.FlipValueRect, flipProperty, Layout.NoGuiContent);

            return hasChanges;
        }
    }
}