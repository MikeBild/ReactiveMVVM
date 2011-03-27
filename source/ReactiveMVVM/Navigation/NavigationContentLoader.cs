using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ReactiveMVVM.Navigation
{
    /*
    <navigation:Frame Source="/View/TodoListPage.xaml" Name="MyNavigationFrame" >
        <navigation:Frame.ContentLoader>            
            <helpers:NavigationContentLoader />            
        </navigation:Frame.ContentLoader>
    </navigation:Frame>
    */
    public class NavigationContentLoader : INavigationContentLoader
    {
        private readonly PageResourceContentLoader _loader = new PageResourceContentLoader();
        private UserControl _currentView;
        private bool _isNavigatingToSameView;
      
        public IAsyncResult BeginLoad(Uri targetUri, Uri currentUri, AsyncCallback userCallback, object asyncState)
        {
            _isNavigatingToSameView = false;
            if (currentUri != null)
            {
                var file1 = Path.GetFileNameWithoutExtension(targetUri.OriginalString);
                var file2 = Path.GetFileNameWithoutExtension(currentUri.OriginalString);

                _isNavigatingToSameView = (file1 == file2);
            }

            return _loader.BeginLoad(targetUri, currentUri, userCallback, asyncState);
        }

        public bool CanLoad(Uri targetUri, Uri currentUri)
        {
            return _loader.CanLoad(targetUri, currentUri);
        }

        public void CancelLoad(IAsyncResult asyncResult)
        {
            _loader.CancelLoad(asyncResult);
        }       

        public LoadResult EndLoad(IAsyncResult asyncResult)
        {
            if (_isNavigatingToSameView)
                return new LoadResult(_currentView);
            
            var loadResult = _loader.EndLoad(asyncResult);

            _currentView = loadResult.LoadedContent as UserControl;
            return loadResult;
        }
    }
}