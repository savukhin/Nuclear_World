from django.db import models
from authentication.models import CustomUser

# Create your models here.
class Item(models.Model):
    name = models.CharField(max_length=100)
   # guid = models.CharField()


class Arms(Item):
    pass


class Body(Item):
    pass


class Boots(Item):
    pass


class Cloack(Item):
    pass


class Head(Item):
    pass


class Legs(Item):
    pass


class Mask(Item):
    pass


class AdditionalWeapon(Item):
    pass


class PrimaryWeapon(Item):
    pass


class Equipment(models.Model):
    arms = models.OneToOneField(to=Arms, on_delete=models.CASCADE)
    body = models.OneToOneField(to=Body, on_delete=models.CASCADE)
    boots = models.OneToOneField(to=Boots, on_delete=models.CASCADE)
    cloack = models.OneToOneField(to=Cloack, on_delete=models.CASCADE)
    head = models.OneToOneField(to=Head, on_delete=models.CASCADE)
    legs = models.OneToOneField(to=Legs, on_delete=models.CASCADE)
    mask = models.OneToOneField(to=Mask, on_delete=models.CASCADE)
    additionalWeapon = models.OneToOneField(to=AdditionalWeapon, on_delete=models.CASCADE)
    primaryWeapon = models.OneToOneField(to=PrimaryWeapon, on_delete=models.CASCADE)


class Inventory(models.Model):
    pass


class InventoryItem(models.Model):
    item = models.OneToOneField(to=Item, on_delete=models.CASCADE)
    inventory = models.OneToOneField(to=Inventory, on_delete=models.CASCADE)
    position = models.IntegerField()


class Character(models.Model):
    name = models.CharField(max_length=20, blank=False)
    exp = models.IntegerField()
    level = models.IntegerField()
    user = models.ForeignKey(to=CustomUser, on_delete=models.CASCADE)
    inventory = models.OneToOneField(to=Inventory, on_delete=models.CASCADE);
    equipment = models.OneToOneField(to=Equipment, on_delete=models.CASCADE);
