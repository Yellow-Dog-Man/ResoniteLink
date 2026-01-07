---
uid: compdocs
title: Using components
---

## Using components

### Attaching a component
Let's attach a component to our new slot!

```JSON
{
    "$type" : "addComponent",
    "containerSlotId" : "MySDK_0",
    "data" : {
        "id" : "MySDK_1",
        "componentType" : "[FrooxEngine]FrooxEngine.Grabbable",
        "members" : {
            "Scalable" : {
                "$type" : "bool",
                "value" : true
            }
        }
    }
}
```

| Field                         | Description                                                                                                                                                                                                                                                                                                                                                                  |
|-------------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `containerSlotId`             | This is MANDATORY for adding components! Resonite needs to know which slot to attach them to.                                                                                                                                                                                                                                                                                |
| `data`                        | This is the component definition. This is same model as you get when querying component data.                                                                                                                                                                                                                                                                                |
| `data.id`                     | This is optional when adding a component, similarly to adding a slot. We provide our own ID so we can easily reference it later.                                                                                                                                                                                                                                             |
| `data.componentType`          | This is also MANDATORY when adding a component - we must specify the type. The syntax is the same you'd use within Resonite when writing types in the componet attacher. You can technically specify this when updating the component later too, but it's NOT RECOMMENDED - if you specify different type, it will fail, because you can't change component type at runtime. |
| `data.members`                | This is a dictionary that allows you to define the values for component's members. Their names match 1:1 to how you'd see them in Resonite inspector.                                                                                                                                                                                                                        |
| `data.members.Scalable.value` | We're setting the Scalable field on the Grabbable component to true. Everything else is left at default.                                                                                                                                                                                                                                                                     |

### Updating fields on components
If you want to update fields on existing components (whether you added them or not), just send a component update! Let's say we changed our mind and we want the component to no longer be Scalable.

```JSON
{
    "$type" : "updateComponent",
    "data" : {
        "id" : "MySDK_1",
        "members" : {
            "Scalable" : {
                "$type" : "bool",
                "value" : false
            }
        }
    }
}
```

| Field     | Description                                                                 |
|-----------|-----------------------------------------------------------------------------|
| `data.id` | Since we're updating existing component, providing its ID is now MANDATORY. |

### Removing Slot
Let's say we want to clean up now and remove the slot we made. This is very easy:

```JSON
{
    "$type" : "removeSlot",
    "slotId" : "MySDK_0"
}
```

| Field    | Description                                                    |
|----------|----------------------------------------------------------------|
| `slotId` | Pretty self-explanatory. The ID of the slot we want to remove! |
