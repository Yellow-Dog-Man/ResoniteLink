---
uid: slotsdocs
title: Manipulating slots
---

## Manipulating slots

### Querying slot data

To fetch information about the scene hierarchy, you can use the `getSlot` message:

```JSON
{
    "$type" : "getSlot",
    "slotId" : "Root",
    "includeComponentData" : false,
    "depth" : 0
}
```

| Field                  | Description                                                                                                                                                                                                                                                                                                                                                        |
|------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `slotId`               | ID of the slot we are fetching. Root slot has special ID "Root", otherwise you get the ID's of other slots as a result of this query.                                                                                                                                                                                                                              |
| `includeComponentData` | When true, the response will contain full data of components on the fetched slots. This can be useful if you want to get the data as single bulk, but can be inefficient if you're fetching it piece by piece. When false, the response will only include reference data for the components (their type & ID). You can fetch data for individual components later. |
| `depth`                | Indicates how deep to fully fetch slots. 0 will only fully fetch the requested slot. Any children will only have basic reference info (Name & ID's). -1 will fetch as deep as possible. Doing -1 at Root slot will fetch the entire scene hierarchy.                                                                                                               |

### Adding a Slot
You can add new slots! This is a bit more involved, as you need to specify more things. However what makes this easier is that any parameters that you omit are left to defaults.

```JSON
{
    "$type" : "addSlot",
    "data" : {
        "id" : "MySDK_0",
        "parent" : {
            "$type" : "reference",
            "targetId" : "Root"
        },
        "name" : {
            "$type" : "string",
            "value" : "Hello from MySDK!"
        },
        "position" : {
            "$type" : "float3",
            "value" : {
                "x" : 0,
                "y" : 1.5,
                "z" : 10
            }
        }
    }
}
```

| Field                  | Description                                                                                                                                                                     |
|------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `data`                 | This provides the actual definition of the Slot. This is the same definition you get when querying slot data too - the model is shared!                                         |
| `data.id`              | The ID of the new Slot. You can provide your own (see section below for details) or you can omit this and let Resonite allocate this.                                           |
| `data.parent.targetId` | The ID of the parent slot for this slot. In this example it's just Root - you could omit this, as Root will be the default, but this is how you can set the parent to any slot. |
| `data.name.value`      | You can name the new slot when creating it!                                                                                                                                     |
| `data.position.value`  | You can position the new slot when creating it too! We omitted rotation, scale and other fields here - they'll just be at defaults.                                             |

### Updating a Slot
In previous example we created a slot. Let's say we want to update its scale. We can send another message, that uses the same schema for slot definition - we only include the properties that we want to change. Anything that's not included will be left as-is.

```JSON
{
    "$type" : "updateSlot",
    "data" : {
        "id" : "MySDK_0",
        "scale" : {
            "$type" : "float3",
            "value" : {
                "x" : 2.5,
                "y" : 2.5,
                "z" : 2.5
            }
        }
    }
}
```

| Field        | Description                                                                                                         |
|--------------|---------------------------------------------------------------------------------------------------------------------|
| `data`       | This is the same model when adding slot or when getting slot data when querying them!                               |
| `data.id`    | For this message, this is MANDATORY! We are updating existing slot, so Resonite needs to know which one to update.  |
| `data.scale` | We're updating the scale of the slot, so it's the only thing we need to include. Everything else can be left as-is. |
