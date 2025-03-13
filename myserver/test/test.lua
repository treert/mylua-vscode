
-------------------------------- class start --------------------------------
---@class BaseCls
local BaseCls = {cname = "BaseCls", super = nil}
BaseCls.__index = BaseCls -- 将要用作元表。需要设置这个

-- 构造函数，**不带参数**，类似UE的CDO的效果。
-- 如果要构造，定义类似Init之类的函数，手动调用。
function BaseCls:ctor()

end

---@generic T
---@param self T
---@return T
function BaseCls:new()
    local o = {}
    setmetatable(o, self)
    o:ctor()
    return o
end

--- 继承框架。（支持多继承，小心使用。）
function class(class_name,base_class,...)
    base_class = base_class or BaseCls
    class_name = class_name or '' -- anonymous class name is empty string
    local cls = {cname = class_name, super = base_class}
    cls.__index = cls

    if ... then
        -- 多继承
        local supers = {base_class, ...}
        setmetatable(cls,{__index = function (t,k)
            for _,super in ipairs(supers) do
                local v = super[k]
                if v ~= nil then
                    return v
                end
            end
        end})
    else
        -- 单继承
        setmetatable(cls,base_class)
    end
    return cls
end

-------------------------------- class end --------------------------------

---@class A:BaseCls
A = class("A")

function A:print()
    print("cname="..self.cname)
end

function A:fa()
    print("call fa")
end

---@class B:BaseCls
B = class("B",BaseCls)

function B:fb()
    print("call fb")
end

---@class C:A,B
C = class("C",B,A)

local a = A:new()
a:print()

local c = C:new()
c:fa()
c:fb()

local str = "asdfa\z 

"

