LEntity = SimpleClass()

function LEntity:__init(uid,data)
    self:__init_self()
    self.data = data 
    self.uid = uid
end 

function LEntity:__init_self()
	self.compPool = { }
	self.obj = nil 
	self.uid = nil 
	self.data = nil 
	----
	self.cc = nil 
	self.anim = nil 
end 

function LEntity:onLoading()
	print("<color=red> 加载实体模型中... </color>"..self.data:getPath())
    LuaExtend:loadObj(self.data:getPath(),Bind(self.onLoadComplete,self))
end 

function LEntity:onLoadComplete(obj)
    if obj then 
    	self.obj = obj    
    	self:initialize()
    end 
end 

function LEntity:initialize()
   if not self.obj then 
   	  return 
   end
   self.cc = self.obj:AddComponent(CharacterController)
   local h = self.data:getCCHeight()
   local radius = self.data:getCCRadius()   
   self.cc.height = h
   self.cc.center = {0,h/2,0}
   self.cc.radius = radius
end 

function LEntity:onBaseDispose()
	self:onDispose()
	self.compPool = nil 
	LuaExtend:destroyObj(self.obj)
	self.obj = nil 
	self.uid = nil 
	self.data = nil 
end 

function LEntity:onDispose()

end 