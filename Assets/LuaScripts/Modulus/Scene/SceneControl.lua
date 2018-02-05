SceneControl = SimpleClass(BaseControl)

function SceneControl:init()

end 

function SceneControl:initEvent()
	EventMgr:addListener(Define.On_Scene_Load_Begin,Bind(self.onSceneLoadBegin,self))
	EventMgr:addListener(Define.On_Scene_Load_Finish,Bind(self.onSceneLoadEnd,self))
end 

function SceneControl:onSceneLoadBegin()
    print("场景加载开始 CS call Lua")
    --TimeMgr:clear()
end 

function SceneControl:onSceneLoadEnd()
    print("场景加载完毕 CS call Lua")
    UIMgr:openUI(UIEnum.FaceBookUI,nil)
end 

Register('SceneControl')