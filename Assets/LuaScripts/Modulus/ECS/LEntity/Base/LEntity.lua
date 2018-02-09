LEntity = SimpleClass()

function LEntity:__init(uid,data)
    self:__init_self()
    self.data = data 
    self.uid = uid
end 

function LEntity:__init_self()
	self.compPool = { }
	self.prefab = nil 
	self.uid = nil 
	self.data = nil 
	self.root = nil 
	----
	self.cc = nil 
	self.anim = nil 
end 

function LEntity:onLoading()	
	self.root = Utils:newObj(tostring(self.uid))
	LuaExtend:setObjPosTable(self.root,self.data:getSpawn())
    LuaExtend:loadObj(self.data:getPath(),Bind(self.onLoadComplete,self))
end 

function LEntity:onLoadComplete(obj)
    if obj then 
    	self.prefab = obj    
    	self:initialize()
    end 
end 

function LEntity:initialize()
   if not self.prefab then 
   	  return 
   end
   self.prefab.transform:SetParent(self.root.transform)
   LuaExtend:setObjPosTable(self.prefab,{0,0,0})
   self.cc = self.root:AddComponent(CharacterController)
   local h = self.data:getCCHeight()
   local radius = self.data:getCCRadius()   
   self.cc.height = h   
   self.cc.center = Vector3(0,h/2,0)
   --self.cc.center = {0,h/2,0}
   self.cc.radius = radius
end 

function LEntity:onBaseDispose()
	self:onDispose()
	self.compPool = nil 
	LuaExtend:destroyObj(self.prefab)
	self.prefab = nil 
	self.uid = nil 
	self.data = nil 
end 

function LEntity:getRoot()
	return self.root
end 

function LEntity:onDispose()

end 