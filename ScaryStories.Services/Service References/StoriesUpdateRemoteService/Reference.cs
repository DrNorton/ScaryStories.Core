﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18051
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This code was auto-generated by Microsoft.Silverlight.Phone.ServiceReference, version 3.7.0.0
// 
namespace ScaryStories.Services.StoriesUpdateRemoteService {
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CategoryServiceDto", Namespace="http://schemas.datacontract.org/2004/07/ScaryStories.UpdateService")]
    public partial class CategoryServiceDto : object, System.ComponentModel.INotifyPropertyChanged {
        
        private System.DateTime CreatedTimeField;
        
        private byte[] ImageField;
        
        private string NameField;
        
        private System.Collections.ObjectModel.ObservableCollection<int> StoriesIdsField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime CreatedTime {
            get {
                return this.CreatedTimeField;
            }
            set {
                if ((this.CreatedTimeField.Equals(value) != true)) {
                    this.CreatedTimeField = value;
                    this.RaisePropertyChanged("CreatedTime");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte[] Image {
            get {
                return this.ImageField;
            }
            set {
                if ((object.ReferenceEquals(this.ImageField, value) != true)) {
                    this.ImageField = value;
                    this.RaisePropertyChanged("Image");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.ObjectModel.ObservableCollection<int> StoriesIds {
            get {
                return this.StoriesIdsField;
            }
            set {
                if ((object.ReferenceEquals(this.StoriesIdsField, value) != true)) {
                    this.StoriesIdsField = value;
                    this.RaisePropertyChanged("StoriesIds");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CheckingResult", Namespace="http://schemas.datacontract.org/2004/07/ScaryStories.UpdateService")]
    public partial class CheckingResult : object, System.ComponentModel.INotifyPropertyChanged {
        
        private long NewStoriesNumberField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long NewStoriesNumber {
            get {
                return this.NewStoriesNumberField;
            }
            set {
                if ((this.NewStoriesNumberField.Equals(value) != true)) {
                    this.NewStoriesNumberField = value;
                    this.RaisePropertyChanged("NewStoriesNumber");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="StoryServiceDto", Namespace="http://schemas.datacontract.org/2004/07/ScaryStories.UpdateService")]
    public partial class StoryServiceDto : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string CategoryNameField;
        
        private System.DateTime CreatedTimeField;
        
        private string HeaderField;
        
        private byte[] ImageField;
        
        private bool IsFavoriteField;
        
        private long LikesField;
        
        private string TextField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CategoryName {
            get {
                return this.CategoryNameField;
            }
            set {
                if ((object.ReferenceEquals(this.CategoryNameField, value) != true)) {
                    this.CategoryNameField = value;
                    this.RaisePropertyChanged("CategoryName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime CreatedTime {
            get {
                return this.CreatedTimeField;
            }
            set {
                if ((this.CreatedTimeField.Equals(value) != true)) {
                    this.CreatedTimeField = value;
                    this.RaisePropertyChanged("CreatedTime");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Header {
            get {
                return this.HeaderField;
            }
            set {
                if ((object.ReferenceEquals(this.HeaderField, value) != true)) {
                    this.HeaderField = value;
                    this.RaisePropertyChanged("Header");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte[] Image {
            get {
                return this.ImageField;
            }
            set {
                if ((object.ReferenceEquals(this.ImageField, value) != true)) {
                    this.ImageField = value;
                    this.RaisePropertyChanged("Image");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsFavorite {
            get {
                return this.IsFavoriteField;
            }
            set {
                if ((this.IsFavoriteField.Equals(value) != true)) {
                    this.IsFavoriteField = value;
                    this.RaisePropertyChanged("IsFavorite");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long Likes {
            get {
                return this.LikesField;
            }
            set {
                if ((this.LikesField.Equals(value) != true)) {
                    this.LikesField = value;
                    this.RaisePropertyChanged("Likes");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Text {
            get {
                return this.TextField;
            }
            set {
                if ((object.ReferenceEquals(this.TextField, value) != true)) {
                    this.TextField = value;
                    this.RaisePropertyChanged("Text");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="StoriesUpdateRemoteService.IStoriesUpdateService")]
    public interface IStoriesUpdateService {
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IStoriesUpdateService/GetNewStories", ReplyAction="http://tempuri.org/IStoriesUpdateService/GetNewStoriesResponse")]
        System.IAsyncResult BeginGetNewStories(System.DateTime lastUpdateTime, System.AsyncCallback callback, object asyncState);
        
        System.Collections.ObjectModel.ObservableCollection<ScaryStories.Services.StoriesUpdateRemoteService.CategoryServiceDto> EndGetNewStories(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IStoriesUpdateService/CheckUpdate", ReplyAction="http://tempuri.org/IStoriesUpdateService/CheckUpdateResponse")]
        System.IAsyncResult BeginCheckUpdate(System.DateTime lastUpdateTime, System.AsyncCallback callback, object asyncState);
        
        ScaryStories.Services.StoriesUpdateRemoteService.CheckingResult EndCheckUpdate(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IStoriesUpdateService/GetNewStory", ReplyAction="http://tempuri.org/IStoriesUpdateService/GetNewStoryResponse")]
        System.IAsyncResult BeginGetNewStory(int id, System.AsyncCallback callback, object asyncState);
        
        ScaryStories.Services.StoriesUpdateRemoteService.StoryServiceDto EndGetNewStory(System.IAsyncResult result);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IStoriesUpdateServiceChannel : ScaryStories.Services.StoriesUpdateRemoteService.IStoriesUpdateService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GetNewStoriesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public GetNewStoriesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public System.Collections.ObjectModel.ObservableCollection<ScaryStories.Services.StoriesUpdateRemoteService.CategoryServiceDto> Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((System.Collections.ObjectModel.ObservableCollection<ScaryStories.Services.StoriesUpdateRemoteService.CategoryServiceDto>)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CheckUpdateCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public CheckUpdateCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public ScaryStories.Services.StoriesUpdateRemoteService.CheckingResult Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((ScaryStories.Services.StoriesUpdateRemoteService.CheckingResult)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GetNewStoryCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public GetNewStoryCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public ScaryStories.Services.StoriesUpdateRemoteService.StoryServiceDto Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((ScaryStories.Services.StoriesUpdateRemoteService.StoryServiceDto)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class StoriesUpdateServiceClient : System.ServiceModel.ClientBase<ScaryStories.Services.StoriesUpdateRemoteService.IStoriesUpdateService>, ScaryStories.Services.StoriesUpdateRemoteService.IStoriesUpdateService {
        
        private BeginOperationDelegate onBeginGetNewStoriesDelegate;
        
        private EndOperationDelegate onEndGetNewStoriesDelegate;
        
        private System.Threading.SendOrPostCallback onGetNewStoriesCompletedDelegate;
        
        private BeginOperationDelegate onBeginCheckUpdateDelegate;
        
        private EndOperationDelegate onEndCheckUpdateDelegate;
        
        private System.Threading.SendOrPostCallback onCheckUpdateCompletedDelegate;
        
        private BeginOperationDelegate onBeginGetNewStoryDelegate;
        
        private EndOperationDelegate onEndGetNewStoryDelegate;
        
        private System.Threading.SendOrPostCallback onGetNewStoryCompletedDelegate;
        
        private BeginOperationDelegate onBeginOpenDelegate;
        
        private EndOperationDelegate onEndOpenDelegate;
        
        private System.Threading.SendOrPostCallback onOpenCompletedDelegate;
        
        private BeginOperationDelegate onBeginCloseDelegate;
        
        private EndOperationDelegate onEndCloseDelegate;
        
        private System.Threading.SendOrPostCallback onCloseCompletedDelegate;
        
        public StoriesUpdateServiceClient() {
        }
        
        public StoriesUpdateServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public StoriesUpdateServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public StoriesUpdateServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public StoriesUpdateServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Net.CookieContainer CookieContainer {
            get {
                System.ServiceModel.Channels.IHttpCookieContainerManager httpCookieContainerManager = this.InnerChannel.GetProperty<System.ServiceModel.Channels.IHttpCookieContainerManager>();
                if ((httpCookieContainerManager != null)) {
                    return httpCookieContainerManager.CookieContainer;
                }
                else {
                    return null;
                }
            }
            set {
                System.ServiceModel.Channels.IHttpCookieContainerManager httpCookieContainerManager = this.InnerChannel.GetProperty<System.ServiceModel.Channels.IHttpCookieContainerManager>();
                if ((httpCookieContainerManager != null)) {
                    httpCookieContainerManager.CookieContainer = value;
                }
                else {
                    throw new System.InvalidOperationException("Unable to set the CookieContainer. Please make sure the binding contains an HttpC" +
                            "ookieContainerBindingElement.");
                }
            }
        }
        
        public event System.EventHandler<GetNewStoriesCompletedEventArgs> GetNewStoriesCompleted;
        
        public event System.EventHandler<CheckUpdateCompletedEventArgs> CheckUpdateCompleted;
        
        public event System.EventHandler<GetNewStoryCompletedEventArgs> GetNewStoryCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> OpenCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> CloseCompleted;
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult ScaryStories.Services.StoriesUpdateRemoteService.IStoriesUpdateService.BeginGetNewStories(System.DateTime lastUpdateTime, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginGetNewStories(lastUpdateTime, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Collections.ObjectModel.ObservableCollection<ScaryStories.Services.StoriesUpdateRemoteService.CategoryServiceDto> ScaryStories.Services.StoriesUpdateRemoteService.IStoriesUpdateService.EndGetNewStories(System.IAsyncResult result) {
            return base.Channel.EndGetNewStories(result);
        }
        
        private System.IAsyncResult OnBeginGetNewStories(object[] inValues, System.AsyncCallback callback, object asyncState) {
            System.DateTime lastUpdateTime = ((System.DateTime)(inValues[0]));
            return ((ScaryStories.Services.StoriesUpdateRemoteService.IStoriesUpdateService)(this)).BeginGetNewStories(lastUpdateTime, callback, asyncState);
        }
        
        private object[] OnEndGetNewStories(System.IAsyncResult result) {
            System.Collections.ObjectModel.ObservableCollection<ScaryStories.Services.StoriesUpdateRemoteService.CategoryServiceDto> retVal = ((ScaryStories.Services.StoriesUpdateRemoteService.IStoriesUpdateService)(this)).EndGetNewStories(result);
            return new object[] {
                    retVal};
        }
        
        private void OnGetNewStoriesCompleted(object state) {
            if ((this.GetNewStoriesCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.GetNewStoriesCompleted(this, new GetNewStoriesCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void GetNewStoriesAsync(System.DateTime lastUpdateTime) {
            this.GetNewStoriesAsync(lastUpdateTime, null);
        }
        
        public void GetNewStoriesAsync(System.DateTime lastUpdateTime, object userState) {
            if ((this.onBeginGetNewStoriesDelegate == null)) {
                this.onBeginGetNewStoriesDelegate = new BeginOperationDelegate(this.OnBeginGetNewStories);
            }
            if ((this.onEndGetNewStoriesDelegate == null)) {
                this.onEndGetNewStoriesDelegate = new EndOperationDelegate(this.OnEndGetNewStories);
            }
            if ((this.onGetNewStoriesCompletedDelegate == null)) {
                this.onGetNewStoriesCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnGetNewStoriesCompleted);
            }
            base.InvokeAsync(this.onBeginGetNewStoriesDelegate, new object[] {
                        lastUpdateTime}, this.onEndGetNewStoriesDelegate, this.onGetNewStoriesCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult ScaryStories.Services.StoriesUpdateRemoteService.IStoriesUpdateService.BeginCheckUpdate(System.DateTime lastUpdateTime, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginCheckUpdate(lastUpdateTime, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ScaryStories.Services.StoriesUpdateRemoteService.CheckingResult ScaryStories.Services.StoriesUpdateRemoteService.IStoriesUpdateService.EndCheckUpdate(System.IAsyncResult result) {
            return base.Channel.EndCheckUpdate(result);
        }
        
        private System.IAsyncResult OnBeginCheckUpdate(object[] inValues, System.AsyncCallback callback, object asyncState) {
            System.DateTime lastUpdateTime = ((System.DateTime)(inValues[0]));
            return ((ScaryStories.Services.StoriesUpdateRemoteService.IStoriesUpdateService)(this)).BeginCheckUpdate(lastUpdateTime, callback, asyncState);
        }
        
        private object[] OnEndCheckUpdate(System.IAsyncResult result) {
            ScaryStories.Services.StoriesUpdateRemoteService.CheckingResult retVal = ((ScaryStories.Services.StoriesUpdateRemoteService.IStoriesUpdateService)(this)).EndCheckUpdate(result);
            return new object[] {
                    retVal};
        }
        
        private void OnCheckUpdateCompleted(object state) {
            if ((this.CheckUpdateCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.CheckUpdateCompleted(this, new CheckUpdateCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void CheckUpdateAsync(System.DateTime lastUpdateTime) {
            this.CheckUpdateAsync(lastUpdateTime, null);
        }
        
        public void CheckUpdateAsync(System.DateTime lastUpdateTime, object userState) {
            if ((this.onBeginCheckUpdateDelegate == null)) {
                this.onBeginCheckUpdateDelegate = new BeginOperationDelegate(this.OnBeginCheckUpdate);
            }
            if ((this.onEndCheckUpdateDelegate == null)) {
                this.onEndCheckUpdateDelegate = new EndOperationDelegate(this.OnEndCheckUpdate);
            }
            if ((this.onCheckUpdateCompletedDelegate == null)) {
                this.onCheckUpdateCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnCheckUpdateCompleted);
            }
            base.InvokeAsync(this.onBeginCheckUpdateDelegate, new object[] {
                        lastUpdateTime}, this.onEndCheckUpdateDelegate, this.onCheckUpdateCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult ScaryStories.Services.StoriesUpdateRemoteService.IStoriesUpdateService.BeginGetNewStory(int id, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginGetNewStory(id, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ScaryStories.Services.StoriesUpdateRemoteService.StoryServiceDto ScaryStories.Services.StoriesUpdateRemoteService.IStoriesUpdateService.EndGetNewStory(System.IAsyncResult result) {
            return base.Channel.EndGetNewStory(result);
        }
        
        private System.IAsyncResult OnBeginGetNewStory(object[] inValues, System.AsyncCallback callback, object asyncState) {
            int id = ((int)(inValues[0]));
            return ((ScaryStories.Services.StoriesUpdateRemoteService.IStoriesUpdateService)(this)).BeginGetNewStory(id, callback, asyncState);
        }
        
        private object[] OnEndGetNewStory(System.IAsyncResult result) {
            ScaryStories.Services.StoriesUpdateRemoteService.StoryServiceDto retVal = ((ScaryStories.Services.StoriesUpdateRemoteService.IStoriesUpdateService)(this)).EndGetNewStory(result);
            return new object[] {
                    retVal};
        }
        
        private void OnGetNewStoryCompleted(object state) {
            if ((this.GetNewStoryCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.GetNewStoryCompleted(this, new GetNewStoryCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void GetNewStoryAsync(int id) {
            this.GetNewStoryAsync(id, null);
        }
        
        public void GetNewStoryAsync(int id, object userState) {
            if ((this.onBeginGetNewStoryDelegate == null)) {
                this.onBeginGetNewStoryDelegate = new BeginOperationDelegate(this.OnBeginGetNewStory);
            }
            if ((this.onEndGetNewStoryDelegate == null)) {
                this.onEndGetNewStoryDelegate = new EndOperationDelegate(this.OnEndGetNewStory);
            }
            if ((this.onGetNewStoryCompletedDelegate == null)) {
                this.onGetNewStoryCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnGetNewStoryCompleted);
            }
            base.InvokeAsync(this.onBeginGetNewStoryDelegate, new object[] {
                        id}, this.onEndGetNewStoryDelegate, this.onGetNewStoryCompletedDelegate, userState);
        }
        
        private System.IAsyncResult OnBeginOpen(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(callback, asyncState);
        }
        
        private object[] OnEndOpen(System.IAsyncResult result) {
            ((System.ServiceModel.ICommunicationObject)(this)).EndOpen(result);
            return null;
        }
        
        private void OnOpenCompleted(object state) {
            if ((this.OpenCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.OpenCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void OpenAsync() {
            this.OpenAsync(null);
        }
        
        public void OpenAsync(object userState) {
            if ((this.onBeginOpenDelegate == null)) {
                this.onBeginOpenDelegate = new BeginOperationDelegate(this.OnBeginOpen);
            }
            if ((this.onEndOpenDelegate == null)) {
                this.onEndOpenDelegate = new EndOperationDelegate(this.OnEndOpen);
            }
            if ((this.onOpenCompletedDelegate == null)) {
                this.onOpenCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnOpenCompleted);
            }
            base.InvokeAsync(this.onBeginOpenDelegate, null, this.onEndOpenDelegate, this.onOpenCompletedDelegate, userState);
        }
        
        private System.IAsyncResult OnBeginClose(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((System.ServiceModel.ICommunicationObject)(this)).BeginClose(callback, asyncState);
        }
        
        private object[] OnEndClose(System.IAsyncResult result) {
            ((System.ServiceModel.ICommunicationObject)(this)).EndClose(result);
            return null;
        }
        
        private void OnCloseCompleted(object state) {
            if ((this.CloseCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.CloseCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void CloseAsync() {
            this.CloseAsync(null);
        }
        
        public void CloseAsync(object userState) {
            if ((this.onBeginCloseDelegate == null)) {
                this.onBeginCloseDelegate = new BeginOperationDelegate(this.OnBeginClose);
            }
            if ((this.onEndCloseDelegate == null)) {
                this.onEndCloseDelegate = new EndOperationDelegate(this.OnEndClose);
            }
            if ((this.onCloseCompletedDelegate == null)) {
                this.onCloseCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnCloseCompleted);
            }
            base.InvokeAsync(this.onBeginCloseDelegate, null, this.onEndCloseDelegate, this.onCloseCompletedDelegate, userState);
        }
        
        protected override ScaryStories.Services.StoriesUpdateRemoteService.IStoriesUpdateService CreateChannel() {
            return new StoriesUpdateServiceClientChannel(this);
        }
        
        private class StoriesUpdateServiceClientChannel : ChannelBase<ScaryStories.Services.StoriesUpdateRemoteService.IStoriesUpdateService>, ScaryStories.Services.StoriesUpdateRemoteService.IStoriesUpdateService {
            
            public StoriesUpdateServiceClientChannel(System.ServiceModel.ClientBase<ScaryStories.Services.StoriesUpdateRemoteService.IStoriesUpdateService> client) : 
                    base(client) {
            }
            
            public System.IAsyncResult BeginGetNewStories(System.DateTime lastUpdateTime, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[1];
                _args[0] = lastUpdateTime;
                System.IAsyncResult _result = base.BeginInvoke("GetNewStories", _args, callback, asyncState);
                return _result;
            }
            
            public System.Collections.ObjectModel.ObservableCollection<ScaryStories.Services.StoriesUpdateRemoteService.CategoryServiceDto> EndGetNewStories(System.IAsyncResult result) {
                object[] _args = new object[0];
                System.Collections.ObjectModel.ObservableCollection<ScaryStories.Services.StoriesUpdateRemoteService.CategoryServiceDto> _result = ((System.Collections.ObjectModel.ObservableCollection<ScaryStories.Services.StoriesUpdateRemoteService.CategoryServiceDto>)(base.EndInvoke("GetNewStories", _args, result)));
                return _result;
            }
            
            public System.IAsyncResult BeginCheckUpdate(System.DateTime lastUpdateTime, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[1];
                _args[0] = lastUpdateTime;
                System.IAsyncResult _result = base.BeginInvoke("CheckUpdate", _args, callback, asyncState);
                return _result;
            }
            
            public ScaryStories.Services.StoriesUpdateRemoteService.CheckingResult EndCheckUpdate(System.IAsyncResult result) {
                object[] _args = new object[0];
                ScaryStories.Services.StoriesUpdateRemoteService.CheckingResult _result = ((ScaryStories.Services.StoriesUpdateRemoteService.CheckingResult)(base.EndInvoke("CheckUpdate", _args, result)));
                return _result;
            }
            
            public System.IAsyncResult BeginGetNewStory(int id, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[1];
                _args[0] = id;
                System.IAsyncResult _result = base.BeginInvoke("GetNewStory", _args, callback, asyncState);
                return _result;
            }
            
            public ScaryStories.Services.StoriesUpdateRemoteService.StoryServiceDto EndGetNewStory(System.IAsyncResult result) {
                object[] _args = new object[0];
                ScaryStories.Services.StoriesUpdateRemoteService.StoryServiceDto _result = ((ScaryStories.Services.StoriesUpdateRemoteService.StoryServiceDto)(base.EndInvoke("GetNewStory", _args, result)));
                return _result;
            }
        }
    }
}